using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaprisaProject.ViewModels;
using CaprisaProject.Models;
using CaprisaProject.DAL;
using Rotativa.MVC;
using Rotativa.Core;
using Microsoft.AspNet.Identity;

namespace CaprisaProject.Controllers
{
    public class HomeController : Controller
    {
      
        private CaprisaContext cp = new CaprisaContext();
        //public static ApplicationDbContext db = new ApplicationDbContext();
        //public static string user = db.Users.ToString();
        
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult AfterLogin()
        {

            return View();
        }
        public ActionResult About()
        {

            return View();
        }
        public ActionResult Sites()
        {

            return View();
        }
        public ActionResult Statistics()
        {
            IQueryable<EnrollmentDateGroup> data = from part in cp.Participants
                                                   group part by part.EnrollmentDate
                                                   into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       ParticipantCount = dateGroup.Count()
                                                   };
            

            return View(data.ToList());
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            cp.Dispose();
            base.Dispose(disposing);
        }
    }
}