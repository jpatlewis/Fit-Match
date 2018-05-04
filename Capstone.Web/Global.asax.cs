using Capstone.Web;
using Capstone.Web.DAL;
using Capstone.Web.DAL.Interfaces;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capstone.Web
{
    // Setting up the application after including Ninject and Ninject.MVC5
    // https://stackoverflow.com/questions/47001124/web-api2-ninjectwebcommon-cs-do-not-appear/47002329#47002329
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            string connectionString = ConfigurationManager.ConnectionStrings["TrainerMatch"].ConnectionString; ; //TODO: Insert Connection String to your database

            kernel.Bind<IUserDAL>().To<UserSQLDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IProfileDAL>().To<ProfileSQLDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IWorkoutDAL>().To<WorkoutSQLDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }
}
