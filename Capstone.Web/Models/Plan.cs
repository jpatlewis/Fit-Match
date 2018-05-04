using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public List<Workout> SeveralWorkouts { get; set; }
        public string PlanName { get; set; }
        public string Notes { get; set; }
        public int ForTrainee { get; set; } //who the workout plan is for
        public int ByTrainer { get; set; } //who assigned the workout plan
        public string TrainerName
        {
            get
            {
                return TrainerFirstName + " " + TrainerLastName;
            }
        }
        public string TrainerFirstName { get; set; }
        public string TrainerLastName { get; set; }
    }
}