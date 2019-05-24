using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;

namespace pt_pal_backend
{
    public static class GetSchedule
    {
        [FunctionName("GetSchedule")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            CloudStorageAccount csa = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureWebJobsStorage"));
            var cbc = csa.CreateCloudBlobClient();
            var container = cbc.GetContainerReference("training-schedules");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(data.Owner.ToLower().Replace(' ', '-') + data.ScheduleName.ToLower().Replace(' ', '-'));
            if (await blob.ExistsAsync())
                return new OkObjectResult(new StreamReader(await blob.OpenRead).ReadToEnd());

            return new OkObjectResult("Could not find the schedule you were looking for");
        }
    }
}
