targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the environment that can be used as part of naming resource convention')
param environmentName string = '${AZURE_ENV_NAME}'

@minLength(1)
@description('Primary location for all resources')
param location string = '${AZURE_LOCATION}'

@description('Name of the resource group')
param resourceGroupName string = 'rg-${AZURE_ENV_NAME}'

@description('Flag to deploy database resources')
param deployDatabase bool = true

// Create resource group
resource rg 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: resourceGroupName
  location: location
  tags: {
    'azd-env-name': environmentName
  }
}

// Deploy main resources
module resources 'main.bicep' = {
  name: 'resources'
  scope: rg
  params: {
    environmentName: environmentName
    location: location
    deployDatabase: deployDatabase
  }
}

// Outputs
output RESOURCE_GROUP_ID string = rg.id
output AZURE_SQL_CONNECTION_STRING string = resources.outputs.AZURE_SQL_CONNECTION_STRING
output BACKEND_URL string = resources.outputs.BACKEND_URL
output FRONTEND_URL string = resources.outputs.FRONTEND_URL
