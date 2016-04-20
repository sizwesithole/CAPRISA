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

namespace CaprisaProject.Controllers
{
    public class EditUserViewModelsController : Controller
    {
        private CaprisaContext db = new CaprisaContext();

        // GET: EditUserViewModels
        public ActionResult Index()
        {
            return View(db.EditUserViewModels.ToList());
        }

        // GET: EditUserViewModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditUserViewModel editUserViewModel = db.EditUserViewModels.Find(id);
            if (editUserViewModel == null)
            {
                return HttpNotFound();
            }
            return View(editUserViewModel);
        }

        // GET: EditUserViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditUserViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,ITid,FullName")] EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                db.EditUserViewModels.Add(editUserViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editUserViewModel);
        }

        // GET: EditUserViewModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditUserViewModel editUserViewModel = db.EditUserViewModels.Find(id);
            if (editUserViewModel == null)
            {
                return HttpNotFound();
            }
            return View(editUserViewModel);
        }

        // POST: EditUserViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,ITid,FullName")] EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editUserViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editUserViewModel);
        }

        // GET: EditUserViewModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditUserViewModel editUserViewModel = db.EditUserViewModels.Find(id);
            if (editUserViewModel == null)
            {
                return HttpNotFound();
            }
            return View(editUserViewModel);
        }

        // POST: EditUserViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EditUserViewModel editUserViewModel = db.EditUserViewModels.Find(id);
            db.EditUserViewModels.Remove(editUserViewModel);
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
