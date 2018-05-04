using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModel
{
    public class PopulatePlanViewModel
    {
        public List<Exercise> Exercises { get; set; }
        public Workout Workout { get; set; }
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public int WorkoutID { get; set; }
    }
}