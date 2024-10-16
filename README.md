# AvailabilityFunction
## A simple Azure Function to check whether a website is up and running

This C# Azure Function can check a website for its availability, and when successful it returns a json output formatted as following:
```
{
  "Code": 200,
  "IsUp": true
}
```
This output is desiged to be consumed by an Azure DevOps Release Gate with the following expression:

```
and(eq(root['IsUp'], 'true'), root['Code'], '200'))
```

If the request isn't successful you will only get the HTTP status code and no output.

## Deployment Instructions

### Prerequisites

- Azure subscription
- GitHub account
- Azure CLI installed

### Steps

1. Clone the repository:
   ```
   git clone https://github.com/MattVSTS/AvailabilityFunction.git
   cd AvailabilityFunction
   ```

2. Create a resource group in Azure:
   ```
   az group create --name <ResourceGroupName> --location <Location>
   ```

3. Deploy the Azure Resource Manager template:
   ```
   az deployment group create --resource-group <ResourceGroupName> --template-file azuredeploy.json
   ```

4. Set up the GitHub Actions CI/CD pipeline:
   - Navigate to your GitHub repository.
   - Go to Actions > New workflow.
   - Select "Set up a workflow yourself".
   - Copy the contents of `.github/workflows/azure-function-deploy.yml` into the editor and save the workflow.

### Files

- `azuredeploy.json`: Azure Resource Manager deployment template for the required infrastructure.
- `.github/workflows/azure-function-deploy.yml`: GitHub Actions CI/CD pipeline configuration for deploying the Azure Function to Azure.
