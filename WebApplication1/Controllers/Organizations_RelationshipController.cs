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
    public class Organizations_RelationshipController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Organizations_Relationship
        public ActionResult Index()
        {
            var organizations_Relationship = db.Organizations_Relationship.Include(o => o.Organization).Include(o => o.Partnership_Types);
            return View(organizations_Relationship.ToList());
        }

        // GET: Organizations_Relationship/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizations_Relationship organizations_Relationship = db.Organizations_Relationship.Find(id);
            if (organizations_Relationship == null)
            {
                return HttpNotFound();
            }
            return View(organizations_Relationship);
        }

        // GET: Organizations_Relationship/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name");
            ViewBag.PartnershipTypeID = new SelectList(db.Partnership_Types, "Partnership_Type_ID", "Partnership_Type_Name");

            Organizations_Relationship sub_p = new Organizations_Relationship();

            sub_p.Organizations_Collection = db.Organizations.ToList();
            sub_p.Partnerships_Collection = db.Partnership_Types.ToList();

            return View(sub_p);
        }

        // POST: Organizations_Relationship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Organization_Relationship_ID,OrganizationID,PartnershipTypeID,Organization_Relationship_Details")] Organizations_Relationship organizations_Relationship)
        {
            if (ModelState.IsValid)
            {
                db.Organizations_Relationship.Add(organizations_Relationship);
                db.SaveChanges();
                return RedirectToAction("Create", "Organizations_Relationship");
            }

            ViewBag.OrganizationID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", organizations_Relationship.OrganizationID);
            ViewBag.PartnershipTypeID = new SelectList(db.Partnership_Types, "Partnership_Type_ID", "Partnership_Type_Name", organizations_Relationship.PartnershipTypeID);
            return View(organizations_Relationship);
        }

        // GET: Organizations_Relationship/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizations_Relationship organizations_Relationship = db.Organizations_Relationship.Find(id);
            if (organizations_Relationship == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", organizations_Relationship.OrganizationID);
            ViewBag.PartnershipTypeID = new SelectList(db.Partnership_Types, "Partnership_Type_ID", "Partnership_Type_Name", organizations_Relationship.PartnershipTypeID);

            organizations_Relationship.Organizations_Collection = db.Organizations.ToList();
            organizations_Relationship.Partnerships_Collection = db.Partnership_Types.ToList();

            return View(organizations_Relationship);
        }

        // POST: Organizations_Relationship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Organization_Relationship_ID,OrganizationID,PartnershipTypeID,Organization_Relationship_Details")] Organizations_Relationship organizations_Relationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizations_Relationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationID = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", organizations_Relationship.OrganizationID);
            ViewBag.PartnershipTypeID = new SelectList(db.Partnership_Types, "Partnership_Type_ID", "Partnership_Type_Name", organizations_Relationship.PartnershipTypeID);
            return View(organizations_Relationship);
        }

        // GET: Organizations_Relationship/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizations_Relationship organizations_Relationship = db.Organizations_Relationship.Find(id);
            if (organizations_Relationship == null)
            {
                return HttpNotFound();
            }
            return View(organizations_Relationship);
        }

        // POST: Organizations_Relationship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organizations_Relationship organizations_Relationship = db.Organizations_Relationship.Find(id);
            db.Organizations_Relationship.Remove(organizations_Relationship);
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
