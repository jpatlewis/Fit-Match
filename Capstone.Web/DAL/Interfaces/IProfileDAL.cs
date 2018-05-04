using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IProfileDAL
    {
        List<User> TrainerProfileSearchPartialName(string name);
        List<User> TrainerProfileSearchFullName(string firstName, string lastName);
        List<User> TrainerProfileSearchPrice(double pricePerHour);
        List<User> TrainerProfileSearchLocation(int location);
    }
}
