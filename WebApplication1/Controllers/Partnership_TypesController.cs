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
    public class Partnership_TypesController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Partnership_Types
        public ActionResult Index()
        {
            return View(db.Partnership_Types.ToList());
        }

        // GET: Partnership_Types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership_Types partnership_Types = db.Partnership_Types.Find(id);
            if (partnership_Types == null)
            {
                return HttpNotFound();
            }
            return View(partnership_Types);
        }

        // GET: Partnership_Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partnership_Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Partnership_Type_ID,Partnership_Type_Name,Partnership_Type_Details")] Partnership_Types partnership_Types)
        {
            if (ModelState.IsValid)
            {
                db.Partnership_Types.Add(partnership_Types);
                db.SaveChanges();
                return RedirectToAction("Create", "Partnership_Types");
            }

            return View(partnership_Types);
        }

        // GET: Partnership_Types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership_Types partnership_Types = db.Partnership_Types.Find(id);
            if (partnership_Types == null)
            {
                return HttpNotFound();
            }
            return View(partnership_Types);
        }

        // POST: Partnership_Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Partnership_Type_ID,Partnership_Type_Name,Partnership_Type_Details")] Partnership_Types partnership_Types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partnership_Types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partnership_Types);
        }

        // GET: Partnership_Types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership_Types partnership_Types = db.Partnership_Types.Find(id);
            if (partnership_Types == null)
            {
                return HttpNotFound();
            }
            return View(partnership_Types);
        }

        // POST: Partnership_Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partnership_Types partnership_Types = db.Partnership_Types.Find(id);
            db.Partnership_Types.Remove(partnership_Types);
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
