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
using System.Linq;

namespace pt_pal_backend
{
    public static class LogWorkout
    {
        [FunctionName("LogWorkout")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            LogExercise data = JsonConvert.DeserializeObject<LogExercise>(requestBody);

            CloudStorageAccount csa = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureWebJobsStorage"));
            var cbc = csa.CreateCloudBlobClient();
            var container = cbc.GetContainerReference("training-schedules");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(data.Owner.ToLower().Replace(' ', '-') + data.ScheduleName.ToLower().Replace(' ', '-'));
            if (!await blob.ExistsAsync())
                return new BadRequestObjectResult("Can't log to a schedule that doesn't exist, please create one first");

            var schedule = JsonConvert.DeserializeObject<ExerciseWeekSchedule>((new StreamReader(await blob.OpenReadAsync())).ReadToEnd());
            var exerciseDay = schedule.ExerciseDays.FirstOrDefault(p => p.Day == (int)DateTime.UtcNow.DayOfWeek);
            var exercise = exerciseDay.ExercisesForToday.First(p => p.Name == data.ExerciseName);
            data.Set.ForEach(p => p.Date = DateTime.Now);
            exercise.Set.AddRange(data.Set);
            await blob.UploadTextAsync(JsonConvert.SerializeObject(exercise));
            return new OkObjectResult("Successfully inserted the workout into the exercise schedule");
        }
    }
}
