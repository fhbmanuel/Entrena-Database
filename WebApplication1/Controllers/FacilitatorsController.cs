using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FacilitatorsController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Facilitators
        public ActionResult Index()
        {
            var facilitators = db.Facilitators.Include(f => f.Gender).Include(f => f.Gender1);
            return View(facilitators.ToList());
        }

        // GET: Facilitators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilitator facilitator = db.Facilitators.Find(id);
            if (facilitator == null)
            {
                return HttpNotFound();
            }
            return View(facilitator);
        }

        // GET: Facilitators/Create
        public ActionResult Create()
        {
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name");
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name");
            return View();
        }

        // POST: Facilitators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Facilitator_ID,Facilitator_Name,Facilitator_Last_Name,Facilitator_Date_Of_Birth,GenderID,Facilitator_Phone,Facilitator_Email,Facilitator_Registration_Date,Facilitator_Biodata")] Facilitator facilitator)
        {
            if (ModelState.IsValid)
            {
                facilitator.Facilitator_Registration_Date = DateTime.Today;
                db.Facilitators.Add(facilitator);
                db.SaveChanges();
                return RedirectToAction("Create","Facilitators");
            }

            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", facilitator.GenderID);
            return View(facilitator);
        }

        // GET: Facilitators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilitator facilitator = db.Facilitators.Find(id);
            if (facilitator == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", facilitator.GenderID);
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", facilitator.GenderID);
            return View(facilitator);
        }

        // POST: Facilitators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Facilitator_ID,Facilitator_Name,Facilitator_Last_Name,Facilitator_Date_Of_Birth,GenderID,Facilitator_Phone,Facilitator_Email,Facilitator_Registration_Date,Facilitator_Biodata")] Facilitator facilitator)
        {
            var excluded = new[] { "Facilitator_Registration_Date" };
            if (ModelState.IsValid)
            {
                db.Entry(facilitator).State = EntityState.Modified;
                foreach (var name in excluded)
                {
                    db.Entry(facilitator).Property(name).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", facilitator.GenderID);
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", facilitator.GenderID);
            return View(facilitator);
        }

        // GET: Facilitators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilitator facilitator = db.Facilitators.Find(id);
            if (facilitator == null)
            {
                return HttpNotFound();
            }
            return View(facilitator);
        }

        // POST: Facilitators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facilitator facilitator = db.Facilitators.Find(id);
            db.Facilitators.Remove(facilitator);
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
