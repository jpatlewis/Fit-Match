using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class CardioExercise : Exercise
    {
        public double Intensity { get; set; } //scale of 1-10
        public int Duration { get; set; }
        public int Cardio_id { get; set; }
    }
}