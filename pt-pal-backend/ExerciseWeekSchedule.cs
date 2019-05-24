using System;
using System.Collections.Generic;
using System.Text;

namespace pt_pal_backend
{
    public class ExerciseWeekSchedule
    {
        public string Owner { get; set; }
        public List<ExerciseDaySchedule> ExerciseDays { get; set; }
    }
}
