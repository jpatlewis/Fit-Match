using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        public List<Plan> PlansList { get; set; } = new List<Plan>();

        public int UserID { get; set; }
    }
}