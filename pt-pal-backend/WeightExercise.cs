using System;
using System.Collections.Generic;
using System.Text;

namespace pt_pal_backend
{
    public class WeightExercise : IExercise
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}
