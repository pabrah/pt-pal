using System;
using System.Collections.Generic;
using System.Text;

namespace pt_pal_backend
{
    public class RunningExercise : IExercise
    {
        public string Name { get; set; }
        public double Speed { get; set; }
        public double Incline { get; set; }
        public bool Interval { get; set; }
    }
}
