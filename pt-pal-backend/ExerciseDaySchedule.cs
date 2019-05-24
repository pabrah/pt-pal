using System;
using System.Collections.Generic;
using System.Text;

namespace pt_pal_backend
{
    public class ExerciseDaySchedule
    {
        public List<Exercise> ExercisesForToday { get; set; }
        public int Day {get;set; }
    }
}
