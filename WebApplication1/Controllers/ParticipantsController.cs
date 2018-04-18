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
    public class ParticipantsController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Participants
        public ActionResult Index()
        {
            var participants = db.Participants.Include(p => p.Gender).Include(p => p.Gender1);
            return View(participants.ToList());
        }

        // GET: Participants/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name");
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name");
            return View();
        }

        // POST: Participants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Participant_ID,Participant_Name,Participant_Last_Name,Participant_Date_Of_Birth,GenderID,Participant_Phone,Participant_Email,Participant_Registration_Date,Participant_Details")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                participant.Participant_Registration_Date = DateTime.Today;
                // -- Check that they actually filled the field.
                db.Participants.Add(participant);
                db.SaveChanges();
                return RedirectToAction("Create", "Participants");
            }

            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", participant.GenderID);
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", participant.GenderID);
            return View(participant);
        }

        // GET: Participants/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", participant.GenderID);
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", participant.GenderID);
            return View(participant);
        }

        // POST: Participants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Participant_ID,Participant_Name,Participant_Last_Name,Participant_Date_Of_Birth,GenderID,Participant_Phone,Participant_Email,Participant_Registration_Date,Participant_Details")] Participant participant)
        {
            var excluded = new[] { "Participant_Registration_Date" };
                        
            if (ModelState.IsValid)
            {                
                db.Entry(participant).State = EntityState.Modified;
                foreach (var name in excluded)
                {
                    db.Entry(participant).Property(name).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", participant.GenderID);
            ViewBag.GenderID = new SelectList(db.Genders, "Gender_ID", "Gender_Name", participant.GenderID);
            return View(participant);
        }

        // GET: Participants/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participant participant = db.Participants.Find(id);
            db.Participants.Remove(participant);
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
