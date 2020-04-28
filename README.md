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
