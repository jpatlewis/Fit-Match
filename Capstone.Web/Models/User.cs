using Capstone.Web.Helpers;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Capstone.Web.Models
{
    public class User
    {

        private const int SALT_LENGTH = 12;

        public int User_ID { get; set; }
        public int? Trainer_ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string User_Location { get; set; }
        public string Salt { get; set; }
        public double Price_Per_Hour { get; set; }
        public int PlanId { get; set; } = 0;

        public User()
        {

        }

        public User(RegisterViewModel user)
        {
            First_Name = user.First_Name;
            Last_Name = user.Last_Name;
            this.Email = user.Email;
            User_Location = user.User_Location;

            byte[] saltString = Security.GenerateSalt(SALT_LENGTH);

            Salt = Convert.ToBase64String(saltString);

            Password = Security.Hash(user.Password, saltString);
        }

        public bool isValidPassword(string password)
        {
            bool isValid = false;

            byte[] saltString = System.Convert.FromBase64String(Salt);
            string testString = Convert.ToBase64String(saltString);

            string hashedInput = Security.Hash(password, saltString);

            if(Password.Equals(hashedInput))
            {
                isValid = true;
            }

            return isValid;
        }


    }
}