using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int Plan_Id { get; set; }
        public List<CardioExercise> RunningAndStuff { get; set; }
        public List<StrengthExercise> GetBig { get; set; }
        public string WorkoutName { get; set; }
        public int WorkoutID { get; set; }
    }
}