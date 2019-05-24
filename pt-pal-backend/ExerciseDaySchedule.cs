using System;
using System.Collections.Generic;
using System.Text;

namespace pt_pal_backend
{
    public class ExerciseDaySchedule
    {
        private int _day;
        public List<Exercise> ExercisesForToday { get; set; }
        public int Day {
            get { return _day; }
            set {
                if (value > 7 || value < 1)
                    throw new NotSupportedException("There is only 7 days a week");
                _day = value;
            }
        }
    }
}
