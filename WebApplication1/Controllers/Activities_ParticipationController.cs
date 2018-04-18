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
    public class Activities_ParticipationController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Activities_Participation
        public ActionResult Index()
        {
            var activities_Participation = db.Activities_Participation.Include(a => a.Activity).Include(a => a.Participant);
            return View(activities_Participation.ToList());
        }

        // GET: Activities_Participation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities_Participation activities_Participation = db.Activities_Participation.Find(id);
            if (activities_Participation == null)
            {
                return HttpNotFound();
            }
            return View(activities_Participation);
        }

        // GET: Activities_Participation/Create
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name");
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name");

            Activities_Participation sub_p = new Activities_Participation();

            sub_p.Participants_Collection = db.Participants.ToList();
            sub_p.Activities_Collection = db.Activities.ToList();

            return View(sub_p);
        }

        // POST: Activities_Participation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Activity_Participation_ID,ActivityID,ParticipantID,Activity_Participation_Details")] Activities_Participation activities_Participation)
        {
            if (ModelState.IsValid)
            {
                db.Activities_Participation.Add(activities_Participation);
                db.SaveChanges();
                return RedirectToAction("Create", "Activities_Participation");
            }

            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name", activities_Participation.ActivityID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name", activities_Participation.ParticipantID);
            return View(activities_Participation);
        }
        

        // GET: Activities_Participation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities_Participation activities_Participation = db.Activities_Participation.Find(id);
            if (activities_Participation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name", activities_Participation.ActivityID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name", activities_Participation.ParticipantID);

            activities_Participation.Participants_Collection = db.Participants.ToList();
            activities_Participation.Activities_Collection = db.Activities.ToList();

            return View(activities_Participation);
        }

        // POST: Activities_Participation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Activity_Participation_ID,ActivityID,ParticipantID,Activity_Participation_Details")] Activities_Participation activities_Participation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activities_Participation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "Activity_ID", "Activity_Name", activities_Participation.ActivityID);
            ViewBag.ParticipantID = new SelectList(db.Participants, "Participant_ID", "Participant_Name", activities_Participation.ParticipantID);
            return View(activities_Participation);
        }

        // GET: Activities_Participation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities_Participation activities_Participation = db.Activities_Participation.Find(id);
            if (activities_Participation == null)
            {
                return HttpNotFound();
            }
            return View(activities_Participation);
        }

        // POST: Activities_Participation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activities_Participation activities_Participation = db.Activities_Participation.Find(id);
            db.Activities_Participation.Remove(activities_Participation);
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
