using System;
using System.Collections.Generic;
using System.Text;

namespace pt_pal_backend
{
    public class LogExercise
    {
        public string Owner { get; set; }
        public string ScheduleName { get; set; }
        public string ExerciseName { get; set; }
        public List<TrainingSet> Set { get; set; }
        public DateTime Date { get; set; }
    }
}
