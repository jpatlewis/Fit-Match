using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserDAL _dal;

        public UserController(IUserDAL dal)
        {
            _dal = dal;
        }
        

        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            User user = _dal.GetCurrentUser(model.Email);
            bool isValidPassword = false;
            
            if (user != null)
            {
                isValidPassword = user.isValidPassword(model.Password);
            }

            //if user does not exist or password is wrong
            if(user == null || !isValidPassword)
            {
                ModelState.AddModelError("invalid credentials", "An invalid username or password was entered");
                return View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
                Session[SessionKeys.Email] = user.Email;
                Session[SessionKeys.UserID] = user.User_ID;
                Session[SessionKeys.Trainer_ID] = user.Trainer_ID;
                Session[SessionKeys.First_Name] = user.First_Name;
                Session[SessionKeys.Last_Name] = user.Last_Name;
            }

            if(user.Trainer_ID != null)
            {                
                return RedirectToAction("Index", "Trainer");      
            }
            
            return RedirectToAction("Index", "Trainee");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            User check = _dal.GetCurrentUser(user.Email);
            if (check != null)
            {
                ModelState.AddModelError("username - exists", "That email address is not available");
                return View("Register", user);
            }

            if(user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("passwords - don't match", "Those passwords did not match");
                return View("Register", user);
            }

            if (user.Is_trainer)
            {
                Trainer newtrainer = new Trainer(user);

                newtrainer.User_Location = user.User_Location;

                if(newtrainer.Additional_notes == null)
                {
                    newtrainer.Additional_notes = "";
                }

                if (newtrainer.Certifications == null)
                {
                    newtrainer.Certifications = "";
                }

                if (newtrainer.Client_Success_Stories == null)
                {
                    newtrainer.Client_Success_Stories = "";
                }

                if(newtrainer.exercise_Philosophy == null)
                {
                    newtrainer.exercise_Philosophy = "";
                }

                bool isAdded = _dal.RegisterUser(newtrainer);

                if(isAdded)
                {
                    LoginViewModel loginVM = new LoginViewModel()
                    {
                        Email = user.Email,
                        Password = user.Password
                    };

                    // TODO: redirect to logged in home page
                    return Login(loginVM);
                }
            } 
            else
            {
                User newUser = new User(user);
                bool isAdded = _dal.RegisterUser(newUser);

                if (isAdded)
                {
                    LoginViewModel loginVM = new LoginViewModel()
                    {
                        Email = user.Email,
                        Password = user.Password,
                    };

                    // TODO: redirect to logged in home page
                    return Login(loginVM);
                }
            }

            return View("Register");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.First_Name);
            Session.Remove(SessionKeys.Email);
            Session.Remove(SessionKeys.Trainer_ID);
            Session.Remove(SessionKeys.UserID);

            return RedirectToAction("Index", "Home");
        }
    }
}