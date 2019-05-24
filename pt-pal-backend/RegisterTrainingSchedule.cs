using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Common;
using Microsoft.Azure;

namespace pt_pal_backend
{
    public static class RegisterTrainingSchedule 
    {
        [FunctionName("RegisterTrainingSchedule")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var regExercise = JsonConvert.DeserializeObject<ExerciseWeekSchedule>(requestBody);

            log.LogInformation("C# HTTP trigger function processed a request.");
            if(regExercise==null || regExercise.ExerciseDays==null || string.IsNullOrEmpty(regExercise.Owner) || regExercise.ExerciseDays.Count==0)
                return new BadRequestObjectResult("Please pass exercises in to register a training\n" + requestBody);

            CloudStorageAccount csa = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureWebJobsStorage"));
            var cbc = csa.CreateCloudBlobClient();
            var container = cbc.GetContainerReference("training-schedules");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(regExercise.Owner.ToLower().Replace(' ','-') + regExercise.ScheduleName.ToLower().Replace(' ','-'));
            if (await blob.ExistsAsync())
                return new BadRequestObjectResult("This schedulename is already taken for someone with this name");

            await blob.UploadTextAsync(requestBody);

            return new OkObjectResult(regExercise);
        }
    }
}
