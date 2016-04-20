using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using System.Data.Entity;
using System.Data;
using DHTMLX.Scheduler.Data;
using DHTMLX.Common;
using CaprisaProject.DAL;
using CaprisaProject.Models;
namespace CaprisaProject.Controllers
{
    public class AppointmentController : Controller
    {
        private CaprisaContext db = new CaprisaContext();
        // GET: Appointment
        [Authorize]
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Terrace;

            scheduler.Config.first_hour = 5;
            scheduler.Config.last_hour = 23;

            //decresea loading time for large amount of eventsd
            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
            return View(scheduler);
        }
        
        //saving content
        public ContentResult Data(DateTime from, DateTime to)
        {
            var apps = db.Appointments.Where(e=>e.StartDate< to && e.EndDate>=from).ToList();
            return new SchedulerAjaxData(apps);
        }

        public ActionResult Save (int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = DHXEventsHelper.Bind<Appointment>(actionValues);
                //set update as default case
                switch(action.Type)
                {
                    case DataActionTypes.Insert:
                        db.Appointments.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        db.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:
                        db.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                db.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch(Exception e)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}