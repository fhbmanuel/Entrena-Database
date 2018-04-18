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
    public class ProjectsController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();
        
        // GET: Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Organization).Include(p => p.Organization1).Include(p => p.Organization2);
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.ProjectDonor1 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name");
            ViewBag.ProjectDonor2 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name");
            ViewBag.ProjectDonor3 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name");

            string query = "select * from Organizations t1, Organizations_Relationship t2, Partnership_Types t3 where t1.Organization_ID = t2.OrganizationID and t2.PartnershipTypeID = t3.Partnership_Type_ID and Partnership_Type_Name = 'Client'";
            Project sub_p = new Project();

            sub_p.Organizations_Collection = db.Organizations.SqlQuery(query).ToList();

            return View(sub_p);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Project_ID,Project_Name,ProjectDonor1,ProjectDonor2,ProjectDonor3,Project_Amount,Project_Details")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Create", "Projects");
            }

            ViewBag.ProjectDonor1 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor1);
            ViewBag.ProjectDonor2 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor2);
            ViewBag.ProjectDonor3 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor3);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectDonor1 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor1);
            ViewBag.ProjectDonor2 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor2);
            ViewBag.ProjectDonor3 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor3);

            project.Organizations_Collection = db.Organizations.ToList();

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Project_ID,Project_Name,ProjectDonor1,ProjectDonor2,ProjectDonor3,Project_Amount,Project_Details")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectDonor1 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor1);
            ViewBag.ProjectDonor2 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor2);
            ViewBag.ProjectDonor3 = new SelectList(db.Organizations, "Organization_ID", "Organization_Name", project.ProjectDonor3);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
