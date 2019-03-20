# AvailabilityFunction
A simple Azure Function to check for a website availability

This function can check a website for its availability, and when successful it returns a json output formatted as such:

{
  "Code": 200,
  "IsUp": true
}
  
This output is desiged to be consumed by an Azure DevOps Release Gate with the following expression:

and(eq(root['IsUp'], 'true'), root['Code'], '200'))

If the request isn't successful you will only get the HTTP status code and no output.
