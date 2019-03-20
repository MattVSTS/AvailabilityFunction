#r "Newtonsoft.Json"

using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;

    try {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(name);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        
        FunctionResponse functionResponse = new FunctionResponse() { 
            Code = (int)response.StatusCode, 
            IsUp = true
        };

        var json = JsonConvert.SerializeObject(functionResponse, Formatting.Indented);

        return name != null
            ? (ActionResult)new OkObjectResult($"{json}")
            : new BadRequestObjectResult("Please pass a name on the query string or in the request body");

        response.Close();
    }

    catch (WebException ex)
    {
        var exceptionResponse = (HttpWebResponse)ex.Response;

        return new StatusCodeResult((int)exceptionResponse.StatusCode);
    }  
}

public class FunctionResponse
{
    public int Code { get; set; }
    public bool IsUp { get; set; } 
}