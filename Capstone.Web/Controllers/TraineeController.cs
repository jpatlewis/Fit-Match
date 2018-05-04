using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Capstone.Web.Controllers
{
    public class TraineeController : Controller
    {
        private IProfileDAL _dal;

        private IWorkoutDAL _dalWorkout;

        private IUserDAL _dalUser;

        public TraineeController(IProfileDAL dal, IWorkoutDAL dalWork, IUserDAL dalUser)
        {
            _dal = dal;
            _dalWorkout = dalWork;
            _dalUser = dalUser;
        }

        // GET: Trainee
        public ActionResult Index()
        {
            if (Session[SessionKeys.UserID] == null)
            {
                return Redirect("/User/Login");
            }

            int ID = (int)Session[SessionKeys.UserID];

            Plan plan = _dalWorkout.GetTraineePlan(ID);

            if(plan != null)
            {
                plan.SeveralWorkouts = _dalWorkout.GetWorkoutsWithExercises(plan.Id);
            }
            
            return View(plan);
        }

        public ActionResult Search(string json)
        {
            if (Session[SessionKeys.UserID] == null)
            {
                return Redirect("/User/Login");
            }

            return View("Search", (object)json);
        }

        [HttpGet]
        public ActionResult SearchResult(string searchString, string searchType)
        {
            List<User> users = null;

            if(searchString == "")
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }

            if (searchType.Equals("name"))
            {
                string[] names = searchString.Split();
                if (names.Length > 1)
                {
                    users = _dal.TrainerProfileSearchFullName(names[0], names[1]);
                }
                else if (names.Length == 1)
                {
                    users = _dal.TrainerProfileSearchPartialName(names[0]);
                }
            }
            else if (searchType.Equals("price"))
            {
                int price = Convert.ToInt32(searchString);
                users = _dal.TrainerProfileSearchPrice(price);
            }
            else if (searchType.Equals("location"))
            {
                int location = Convert.ToInt32(searchString);
                users = _dal.TrainerProfileSearchLocation(location);
            }
                
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrainerProfile(string id)
        {
            if (Session[SessionKeys.UserID] == null)
            {
                return Redirect("/User/Login");
            }

            if (id == null)
            {
                return Redirect("/Trainee/Search");
            }

            Trainer Searchedtrainer = _dalUser.GetTrainer(Convert.ToInt32(id));
            Searchedtrainer.ClientList = _dalUser.GetClients(Convert.ToInt32(id));
            return View("TrainerProfile", Searchedtrainer);
        }

        [HttpGet]
        public ActionResult MatchTrainer(int id)
        {
            int traineeID = (int)Session[SessionKeys.UserID];
            int trainerID = id;

            bool isMatched = _dalUser.MatchWithTrainer(trainerID, traineeID);
            
            if(isMatched)
            {
                TempData["IsMatched"] = true;
            }
            else
            {
                TempData["IsMatched"] = false;
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}