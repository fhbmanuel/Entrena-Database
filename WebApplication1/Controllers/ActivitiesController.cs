using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class ActivitiesController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.Beneficiary_Types).Include(a => a.Facilitator).Include(a => a.Facilitator4).Include(a => a.Facilitator5).Include(a => a.Organization).Include(a => a.Location).Include(a => a.Project).Include(a => a.Sub_Products);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.BeneficiaryTypeID = new SelectList(db.Beneficiary_Types, "Beneficiary_Type_ID", "Beneficiary_Type_Name");
            ViewBag.Facilitator1 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name");
            ViewBag.Facilitator2 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name");
            ViewBag.Facilitator3 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name");
            ViewBag.ImplementingPartnerID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name");
            ViewBag.LocationID = new SelectList(db.Locations, "Location_ID", "Location_Name");
            ViewBag.ProjectID = new SelectList(db.Projects, "Project_ID", "Project_Name");
            ViewBag.SubProductID = new SelectList(db.Sub_Products, "Sub_Product_ID", "Sub_Product_Name");

            Activity sub_p = new Activity();
            string query = "select * from Organizations t1, Organizations_Relationship t2, Partnership_Types t3 where t1.Organization_ID = t2.OrganizationID and t2.PartnershipTypeID = t3.Partnership_Type_ID and Partnership_Type_Name = 'Implementing Partner'";

            sub_p.Starting_Date = DateTime.Now;
            
            sub_p.Sub_Products_Collection = db.Sub_Products.ToList();
            sub_p.Facilitator1_Collection = db.Facilitators.ToList();
            sub_p.Implementing_Organization_Collection = db.Organizations.SqlQuery(query).ToList();
            sub_p.Project_Collection = db.Projects.ToList();
            sub_p.Location_Collection = db.Locations.ToList();
            sub_p.Beneficiary_Type_Collection = db.Beneficiary_Types.ToList();

            return View(sub_p);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Activity_ID,Activity_Name,SubProductID,Facilitator1,Facilitator2,Facilitator3,ImplementingPartnerID,LocationID,ProjectID,BeneficiaryTypeID,Starting_Date,Ending_Date,Activity_Details")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Create", "Activities");
            }

            ViewBag.BeneficiaryTypeID = new SelectList(db.Beneficiary_Types, "Beneficiary_Type_ID", "Beneficiary_Type_Name", activity.BeneficiaryTypeID);
            ViewBag.Facilitator1 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator1);
            ViewBag.Facilitator2 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator2);
            ViewBag.Facilitator3 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator3);
            ViewBag.ImplementingPartnerID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", activity.ImplementingPartnerID);
            ViewBag.LocationID = new SelectList(db.Locations, "Location_ID", "Location_Name", activity.LocationID);
            ViewBag.ProjectID = new SelectList(db.Projects, "Project_ID", "Project_Name", activity.ProjectID);
            ViewBag.SubProductID = new SelectList(db.Sub_Products, "Sub_Product_ID", "Sub_Product_Name", activity.SubProductID);
            
            return View(activity);
        }
        
    // GET: Activities/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeneficiaryTypeID = new SelectList(db.Beneficiary_Types, "Beneficiary_Type_ID", "Beneficiary_Type_Name", activity.BeneficiaryTypeID);
            ViewBag.Facilitator1 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator1);
            ViewBag.Facilitator2 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator2);
            ViewBag.Facilitator3 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator3);
            ViewBag.ImplementingPartnerID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", activity.ImplementingPartnerID);
            ViewBag.LocationID = new SelectList(db.Locations, "Location_ID", "Location_Name", activity.LocationID);
            ViewBag.ProjectID = new SelectList(db.Projects, "Project_ID", "Project_Name", activity.ProjectID);
            ViewBag.SubProductID = new SelectList(db.Sub_Products, "Sub_Product_ID", "Sub_Product_Name", activity.SubProductID);
            
            activity.Sub_Products_Collection = db.Sub_Products.ToList();
            activity.Facilitator1_Collection = db.Facilitators.ToList();
            activity.Implementing_Organization_Collection = db.Organizations.ToList();
            activity.Project_Collection = db.Projects.ToList();
            activity.Location_Collection = db.Locations.ToList();
            activity.Beneficiary_Type_Collection = db.Beneficiary_Types.ToList();

            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Activity_ID,Activity_Name,SubProductID,Facilitator1,Facilitator2,Facilitator3,ImplementingPartnerID,LocationID,ProjectID,BeneficiaryTypeID,Starting_Date,Ending_Date,Activity_Details")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeneficiaryTypeID = new SelectList(db.Beneficiary_Types, "Beneficiary_Type_ID", "Beneficiary_Type_Name", activity.BeneficiaryTypeID);
            ViewBag.Facilitator1 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator1);
            ViewBag.Facilitator2 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator2);
            ViewBag.Facilitator3 = new SelectList(db.Facilitators, "Facilitator_ID", "Facilitator_Name", activity.Facilitator3);
            ViewBag.ImplementingPartnerID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", activity.ImplementingPartnerID);
            ViewBag.LocationID = new SelectList(db.Locations, "Location_ID", "Location_Name", activity.LocationID);
            ViewBag.ProjectID = new SelectList(db.Projects, "Project_ID", "Project_Name", activity.ProjectID);
            ViewBag.SubProductID = new SelectList(db.Sub_Products, "Sub_Product_ID", "Sub_Product_Name", activity.SubProductID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
