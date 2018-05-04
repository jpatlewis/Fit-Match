using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProfileDAL _dal;

        public HomeController(IProfileDAL dal)
        {
            _dal = dal;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

    }
}