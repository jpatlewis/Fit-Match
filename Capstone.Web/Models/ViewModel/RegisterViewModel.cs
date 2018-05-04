using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Capstone.Web.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        

        [Required(ErrorMessage = "Passwords did not match")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public bool Is_trainer { get; set; } = false;

        public string Additional_notes { get; set; } = "";
        public double PricePerHour { get; set; } = 0.0;
        public int YearsExp { get; set; } = 0;
        public string Philosophy { get; set; } = "";
        public string ClientSuccessStories { get; set; } = "";
        public string Certifications { get; set; } = "";
        public string User_Location { get; set; } = "";
    }
}