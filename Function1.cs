//#r "System.Data.SqlClient"
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using Microsoft.Data.SqlClient;
// https://devblogs.microsoft.com/dotnet/introducing-the-new-microsoftdatasqlclient/
// https://docs.microsoft.com/fr-fr/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=netframework-4.8&viewFallbackFrom=netcore-3.1
// https://stackoverflow.com/questions/35444487/how-to-use-sqlclient-in-asp-net-core: "As per Mozarts answer, if you are using .NET Core 3+, reference Microsoft.Data.SqlClient instead."
// => ajout
using System.Configuration;
// https://stackoverflow.com/questions/39854081/is-azure-configurationmanager-compatible-with-net-core "Azure.ConfigurationManager is not compatible with .NET Core."; "Also as a replacement you can use" System.Configuration.ConfigurationManager
using Microsoft.WindowsAzure;

//namespace FunctionApp1
//{
//    public static class Function1
//    {
//        [FunctionName("Function1")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
//            ILogger log)
//        {
//            log.LogInformation("C# HTTP trigger function processed a request.");

//            string name = req.Query["name"];

//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            name = name ?? data?.name;

//            return name != null
//                ? (ActionResult)new OkObjectResult($"Hello, {name}")
//                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
//        }
//    }
//}


//using System.Data.SqlClient;

namespace FunctionApp1 {
    public static class Function1 {

        private static SqlConnection connection = new SqlConnection();

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log) {
            log.LogInformation("Traitement de requête reçue par la fonction trigger C# HTTP.");

            string name = req.Query["name"];
            string connectionString = $"Server=tcp:emmanuelgorandsql.database.windows.net,1433;Initial Catalog=EssaiAzureSql;Persist Security Info=False;User ID=Emmanuel;Password=mexzi2-hazxuj-mamzoZ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var cnnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            // return name != null
            //     ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //     : new BadRequestObjectResult("Veuillez passer un nom dans la chaîne ou le corps de la requête");

            return (ActionResult)new OkObjectResult($"Chaîne de connexion: {connectionString}");
        }
    }
}
