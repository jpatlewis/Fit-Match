using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class TrainerClients
    {
        public int TrainerId { get; set; }
        public List<User> ClientsWithoutPlans { get; set; }
        public List<User> ClientsWithPlans { get; set; }
    }
}