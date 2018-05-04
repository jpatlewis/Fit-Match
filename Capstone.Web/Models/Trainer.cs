using Capstone.Web.Helpers;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Trainer : User
    {

        private const int SALT_LENGTH = 12;

        public int Rating { get; set; } = 5; //not part of MVP, for future use. Scale of 1-5
        public string Additional_notes { get; set; } = "none"; //Lengthy
        public int experience { get; set; } = 0;
        public string exercise_Philosophy { get; set; } = "none";
        public string Client_Success_Stories { get; set; } = "none";
        public List<string> ListCertifications { get; set; } = new List<string>();
        public string Certifications { get; set; } = "none";
        public bool Searchable { get; set; }
        public List<User> ClientList { get; set; }

        public Trainer(RegisterViewModel user)
        {
            First_Name = user.First_Name;
            Last_Name = user.Last_Name;
            this.Email = user.Email;
            Additional_notes = user.Additional_notes;
            Price_Per_Hour = user.PricePerHour;
            experience = user.YearsExp;
            exercise_Philosophy = user.Philosophy;
            Client_Success_Stories = user.ClientSuccessStories;
            Certifications = user.Certifications;

            byte[] saltString = Security.GenerateSalt(SALT_LENGTH);

            Salt = Convert.ToBase64String(saltString);

            Password = Security.Hash(user.Password, saltString);
        }

        public Trainer()
        {

        }
    }
}