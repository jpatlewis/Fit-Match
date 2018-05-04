using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public int Type { get; set; }

        public string Description { get; set; }
        public string VideoLink { get; set; } = "";
        public int TrainerID { get; set; }
        public int WorkoutID { get; set; }
    }
}