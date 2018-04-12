using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.IO;

namespace FunctionAppVS2017
{
    public static class HelloHttp
    {
        [FunctionName("HelloHttp")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req,
            TraceWriter log,
            ExecutionContext context)
        {
            log.Info("C# HTTP trigger function processed a request.");
            log.Info($"From class library: {MyClassLibrary.Hello.SayHello("David")}");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            name = name ?? data?.name;

            if (name == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
            }

            // Read the template file from the Functions folder
            string templateFile = Path.Combine(context.FunctionAppDirectory, "Data", "HelloHttpOutputTemplate.txt");
            string template = File.ReadAllText(templateFile);

            return req.CreateResponse(HttpStatusCode.OK, string.Format(template, name));
        }
    }
}
