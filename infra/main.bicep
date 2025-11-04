@description('The base name for all resources')
param baseName string = 'learnercenter'

@description('The location for all resources')
param location string = 'centralus'

@description('The environment name (dev, test, prod)')
param environment string = 'prod'

@description('The PostgreSQL connection string for Supabase')
@secure()
param postgresqlConnectionString string

// Create resource group scope resources
targetScope = 'resourceGroup'

// App Service Plan for hosting the backend API
resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = {
  name: '${baseName}-plan-${environment}'
  location: location
  properties: {
    reserved: false // Windows
  }
  sku: {
    name: 'B1' // Basic tier for cost efficiency
    tier: 'Basic'
  }
}

// App Service for the backend API
resource appService 'Microsoft.Web/sites@2024-04-01' = {
  name: '${baseName}-api-${environment}'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      netFrameworkVersion: 'v9.0'
      use32BitWorkerProcess: false
      alwaysOn: true
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Production'
        }
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          connectionString: postgresqlConnectionString
          type: 'Custom'
        }
      ]
    }
  }
}

// Static Web App for the frontend
resource staticWebApp 'Microsoft.Web/staticSites@2023-12-01' = {
  name: '${baseName}-frontend-${environment}'
  location: location
  properties: {
    buildProperties: {
      appLocation: '/frontend'
      outputLocation: 'build'
    }
    repositoryUrl: 'https://github.com/MleimanNymbl/LearnerCenter'
    branch: 'master'
  }
  sku: {
    name: 'Free'
    tier: 'Free'
  }
}

// Configure Static Web App settings
resource staticWebAppSettings 'Microsoft.Web/staticSites/config@2023-12-01' = {
  parent: staticWebApp
  name: 'appsettings'
  properties: {
    REACT_APP_API_URL: 'https://${appService.properties.defaultHostName}'
  }
}

// Output the URLs for easy access
output apiUrl string = 'https://${appService.properties.defaultHostName}'
output frontendUrl string = 'https://${staticWebApp.properties.defaultHostname}'
output appServiceName string = appService.name
output staticWebAppName string = staticWebApp.name
