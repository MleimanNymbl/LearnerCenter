@description('The environment name')
param environmentName string

@description('The Azure region where resources will be deployed')
param location string = resourceGroup().location

@description('Flag to deploy database resources')
param deployDatabase bool = true

@description('SQL Server administrator password')
@secure()
param sqlAdminPassword string = newGuid()

// Generate unique suffixes
var resourceSuffix = uniqueString(resourceGroup().id)
var appServiceName = 'backend-${take(resourceSuffix, 8)}'
var staticWebAppName = 'swa-${environmentName}-frontend-${resourceSuffix}'
var sqlServerName = 'sql-${environmentName}-${resourceSuffix}'
var sqlDatabaseName = 'sqldb-${environmentName}'
var keyVaultName = 'kv${environmentName}${take(resourceSuffix, 8)}'
var appInsightsName = 'ai-${environmentName}-${resourceSuffix}'
var logWorkspaceName = 'law-${environmentName}-${resourceSuffix}'
var managedIdentityName = 'mi-${environmentName}-${resourceSuffix}'
var containerRegistryName = 'cr${environmentName}${take(resourceSuffix, 8)}'

// Create User-Assigned Managed Identity
resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: managedIdentityName
  location: location
}

// Create Log Analytics Workspace
resource logWorkspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}

// Create Application Insights
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logWorkspace.id
  }
}

// Create Key Vault
resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: keyVaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: tenant().tenantId
    enabledForTemplateDeployment: true
    enableRbacAuthorization: true
    enablePurgeProtection: true
    enableSoftDelete: true
    softDeleteRetentionInDays: 7
  }
}

// Create SQL Server (conditional)
resource sqlServer 'Microsoft.Sql/servers@2023-08-01-preview' = if (deployDatabase) {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: 'sqladmin'
    administratorLoginPassword: sqlAdminPassword
    publicNetworkAccess: 'Enabled'
  }
  
  // Allow Azure services to access
  resource firewallRule 'firewallRules' = {
    name: 'AllowAzureServices'
    properties: {
      startIpAddress: '0.0.0.0'
      endIpAddress: '0.0.0.0'
    }
  }
  
  // Allow Container App outbound IP
  resource containerAppFirewallRule 'firewallRules' = {
    name: 'AllowContainerApp'
    properties: {
      startIpAddress: '20.161.234.241'
      endIpAddress: '20.161.234.241'
    }
  }
}

// Create SQL Database (conditional)
resource sqlDatabase 'Microsoft.Sql/servers/databases@2023-08-01-preview' = if (deployDatabase) {
  parent: sqlServer
  name: sqlDatabaseName
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
    capacity: 5
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
  }
}

// Store SQL connection string in Key Vault (only if database is deployed)
resource sqlConnectionStringSecret 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = if (deployDatabase) {
  parent: keyVault
  name: 'SqlConnectionString'
  properties: {
    value: 'Server=${sqlServer.name}.${environment().suffixes.sqlServerHostname};Database=${sqlDatabaseName};User Id=sqladmin;Password=${sqlAdminPassword};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
  }
}

// Create Container Registry
resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' = {
  name: containerRegistryName
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}

// Create Container Apps Environment
resource containerAppsEnv 'Microsoft.App/managedEnvironments@2024-03-01' = {
  name: 'cae-${environmentName}-${resourceSuffix}'
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logWorkspace.properties.customerId
        sharedKey: logWorkspace.listKeys().primarySharedKey
      }
    }
  }
}

// Create Container App for Backend
resource containerApp 'Microsoft.App/containerApps@2024-03-01' = {
  name: appServiceName
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${managedIdentity.id}': {}
    }
  }
  tags: {
    'azd-service-name': 'backend'
  }
  properties: {
    managedEnvironmentId: containerAppsEnv.id
    configuration: {
      ingress: {
        external: true
        targetPort: 8080
        allowInsecure: false
      }
      registries: [
        {
          server: containerRegistry.properties.loginServer
          identity: managedIdentity.id
        }
      ]
      secrets: []
    }
    template: {
      containers: [
        {
          name: 'backend'
          image: '${containerRegistry.properties.loginServer}/learnercenter/backend:latest'
          resources: {
            cpu: json('0.25')
            memory: '0.5Gi'
          }
          env: [
            {
              name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
              value: appInsights.properties.ConnectionString
            }
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: 'Production'
            }
            {
              name: 'ConnectionStrings__DefaultConnection'
              value: deployDatabase ? 'Server=tcp:sql-learnercenter-kp7ijvoutfzbo.database.windows.net,1433;Database=sqldb-learnercenter;User ID=sqladmin;Password=TempPassword123!;Encrypt=true;Connection Timeout=30;' : ''
            }
          ]
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 1
      }
    }
  }
}

// Create Static Web App for Frontend
resource staticWebApp 'Microsoft.Web/staticSites@2023-12-01' = {
  name: staticWebAppName
  location: location
  tags: {
    'azd-service-name': 'frontend'
  }
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
  properties: {
    buildProperties: {
      appLocation: '/'
      apiLocation: ''
      outputLocation: 'build'
    }
  }
  
  // Configure app settings for Static Web App
  resource config 'config' = {
    name: 'appsettings'
    properties: {
      REACT_APP_API_URL: 'https://${containerApp.properties.configuration.ingress.fqdn}'
    }
  }
}

// Grant Managed Identity access to Key Vault
resource keyVaultAccessPolicy 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: keyVault
  name: guid(keyVault.id, managedIdentity.id, 'Key Vault Secrets User')
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '4633458b-17de-408a-b874-0445c86b69e6') // Key Vault Secrets User
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

// Grant Managed Identity access to Container Registry
resource containerRegistryAccessPolicy 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: containerRegistry
  name: guid(containerRegistry.id, managedIdentity.id, 'AcrPull')
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d') // AcrPull
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

// Outputs
output AZURE_SQL_CONNECTION_STRING string = deployDatabase ? '@Microsoft.KeyVault(VaultName=${keyVaultName};SecretName=SqlConnectionString)' : ''
output BACKEND_URL string = 'https://${containerApp.properties.configuration.ingress.fqdn}'
output FRONTEND_URL string = 'https://${staticWebApp.properties.defaultHostname}'
output APPLICATIONINSIGHTS_CONNECTION_STRING string = appInsights.properties.ConnectionString
output RESOURCE_GROUP_ID string = resourceGroup().id
output SQL_SERVER_NAME string = deployDatabase ? sqlServerName : ''
output SQL_DATABASE_NAME string = deployDatabase ? sqlDatabaseName : ''
output KEY_VAULT_NAME string = keyVaultName
output AZURE_CONTAINER_REGISTRY_ENDPOINT string = containerRegistry.properties.loginServer
