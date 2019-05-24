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

namespace pt_pal_backend
{
    public static class RegisterTrainingSchedule 
    {
        [FunctionName("RegisterTrainingSchedule")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] ExerciseWeekSchedule regExercise,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            if(regExercise==null || regExercise.ExerciseDays==null || string.IsNullOrEmpty(regExercise.Owner) || regExercise.ExerciseDays.Count==0)
                return new BadRequestObjectResult("Please pass exercises in to register a training");

            return new OkObjectResult(regExercise);
        }
    }
}
