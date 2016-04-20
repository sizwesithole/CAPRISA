using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaprisaProject.DAL;
using CaprisaProject.Models;
using System.Data.Entity.Infrastructure;

namespace CaprisaProject.Controllers
{
    public class ParticipantsController : Controller
    {
        private CaprisaContext db = new CaprisaContext();

        // GET: Participants
        //public ActionResult ParticipantsList()
        //{
        //    return View(db.Participants.ToList());
        //}

        public ActionResult GetParticipants()
        {
            List<Participant> part = db.Participants.ToList();
            return View(part);
        }
        [Authorize]
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.SiteSortParm = String.IsNullOrEmpty(sortOrder) ? "site_desc" : "";
            var participant = from p in db.Participants
                              select p;

            if(!String.IsNullOrEmpty(searchString))
            {
                participant = participant.Where(p => p.ParticipantCode.Contains(searchString) || p.PhoneNumber.Contains(searchString));
                
            }
            
                
            switch (sortOrder)
            {
                case "name_desc":
                    participant = participant.OrderBy(p => p.ParticipantCode);
                    break;
                case "date_desc":
                    participant = participant.OrderBy(p => p.EnrollmentDate);
                    break;
                case "site_desc":
                    participant = participant.OrderBy(p => p.Site);
                    break;
                //default:
                //    participant = participant.OrderBy(p => p.PhoneNumber);
                //    break;
            }
            return View(participant.ToList());
        }

        // GET: Participants/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }
        
        // GET: Participants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Participants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParticipantCode,EnrollmentDate,PhoneNumber,Site")] Participant participant)
        {
            try {
                if (ModelState.IsValid)
                {
                    db.Participants.Add(participant);
                    db.SaveChanges();
                    return RedirectToAction("Create", "Enrollments");
                }
            }
            catch(RetryLimitExceededException)
            {
                ModelState.AddModelError("","Unable to save changes, please try again, or if the problem persists, contact your system admin/developer");
            }
            return View(participant);
        }

        // GET: Participants/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        //Generate PDF
        public ActionResult GeneratePDF()
        {
            return new Rotativa.ActionAsPdf("GetParticipants");
        }
        // POST: Participants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParticipantCode,EnrollmentDate,PhoneNumber,Site")] Participant participant)
        {
            try {
                if (ModelState.IsValid)
                {
                    db.Entry(participant).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes, please try again, or if the problem persists, contact your system admin/developer");
            }
            return View(participant);
        }

        // GET: Participants/Delete/5
        public ActionResult Delete(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete Failed, Try Again, and if problem persists, contact your system admin.";
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try {
                Participant participant = db.Participants.Find(id);
                db.Participants.Remove(participant);
                db.SaveChanges();
            }
            catch(RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to delete, please try again, if problem persists, contact system admin");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
