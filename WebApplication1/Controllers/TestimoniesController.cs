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
    public class TestimoniesController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Testimonies
        public ActionResult Index()
        {
            var testimonies = db.Testimonies.Include(t => t.Activity).Include(t => t.Participant);
            return View(testimonies.ToList());
        }

        // GET: Testimonies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimony testimony = db.Testimonies.Find(id);
            if (testimony == null)
            {
                return HttpNotFound();
            }
            return View(testimony);
        }

        // GET: Testimonies/Create
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name");
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name");
            
            Testimony sub_p = new Testimony();

            sub_p.Participant_Collection = db.Participants.ToList();
            sub_p.Activities_Collection = db.Activities.ToList();

            return View(sub_p);
        }
        
        // POST: Testimonies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Testimony_ID,ActivityID,ParticipantID,Testimony_Details")] Testimony testimony)
        {
            if (ModelState.IsValid)
            {
                db.Testimonies.Add(testimony);
                db.SaveChanges();
                return RedirectToAction("Create", "Testimonies");
            }

            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name", testimony.ActivityID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name", testimony.ParticipantID);
            return View(testimony);
        }

        // GET: Testimonies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimony testimony = db.Testimonies.Find(id);
            if (testimony == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name", testimony.ActivityID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name", testimony.ParticipantID);

            testimony.Participant_Collection = db.Participants.ToList();
            testimony.Activities_Collection = db.Activities.ToList();

            return View(testimony);
        }

        // POST: Testimonies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Testimony_ID,ActivityID,ParticipantID,Testimony_Details")] Testimony testimony)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testimony).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name", testimony.ActivityID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name", testimony.ParticipantID);

            

            return View(testimony);
        }

        // GET: Testimonies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimony testimony = db.Testimonies.Find(id);
            if (testimony == null)
            {
                return HttpNotFound();
            }
            return View(testimony);
        }

        // POST: Testimonies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Testimony testimony = db.Testimonies.Find(id);
            db.Testimonies.Remove(testimony);
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
