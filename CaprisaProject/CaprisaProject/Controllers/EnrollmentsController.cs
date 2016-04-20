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
    public class EnrollmentsController : Controller
    {
        private CaprisaContext db = new CaprisaContext();

        // GET: Enrollments
        [Authorize]
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Participant).Include(e => e.Study);
            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.ParticipantCode = new SelectList(db.Participants, "ParticipantCode", "ParticipantCode");
            ViewBag.StudyCode = new SelectList(db.Studies, "StudyCode", "StudyCode");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,StudyCode,ParticipantCode,Site")] Enrollment enrollment)
        {
            try {
                if (ModelState.IsValid)
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes for Create Enrollment, please try again or contact system admin");
            }
            ViewBag.ParticipantCode = new SelectList(db.Participants, "ParticipantCode", "ParticipantCode", enrollment.ParticipantCode);
            ViewBag.StudyCode = new SelectList(db.Studies, "StudyCode", "StudyCode", enrollment.StudyCode);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParticipantCode = new SelectList(db.Participants, "ParticipantCode", "ParticipantCode", enrollment.ParticipantCode);
            ViewBag.StudyCode = new SelectList(db.Studies, "StudyCode", "StudyCode", enrollment.StudyCode);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,StudyCode,ParticipantCode,Site")] Enrollment enrollment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(enrollment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes, please try again, or if the problem persists, contact your system admin/developer");
            }
            ViewBag.ParticipantCode = new SelectList(db.Participants, "ParticipantCode", "ParticipantCode", enrollment.ParticipantCode);
            ViewBag.StudyCode = new SelectList(db.Studies, "StudyCode", "StudyCode", enrollment.StudyCode);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
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
