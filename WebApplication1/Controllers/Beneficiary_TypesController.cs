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
    public class Beneficiary_TypesController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Beneficiary_Types
        public ActionResult Index()
        {
            return View(db.Beneficiary_Types.ToList());
        }

        // GET: Beneficiary_Types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary_Types beneficiary_Types = db.Beneficiary_Types.Find(id);
            if (beneficiary_Types == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary_Types);
        }

        // GET: Beneficiary_Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beneficiary_Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Beneficiary_Type_ID,Beneficiary_Type_Name,Beneficiary_Type_Details")] Beneficiary_Types beneficiary_Types)
        {
            if (ModelState.IsValid)
            {
                db.Beneficiary_Types.Add(beneficiary_Types);
                db.SaveChanges();
                return RedirectToAction("Create", "Beneficiary_Types");
            }

            return View(beneficiary_Types);
        }

        // GET: Beneficiary_Types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary_Types beneficiary_Types = db.Beneficiary_Types.Find(id);
            if (beneficiary_Types == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary_Types);
        }

        // POST: Beneficiary_Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Beneficiary_Type_ID,Beneficiary_Type_Name,Beneficiary_Type_Details")] Beneficiary_Types beneficiary_Types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiary_Types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beneficiary_Types);
        }

        // GET: Beneficiary_Types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary_Types beneficiary_Types = db.Beneficiary_Types.Find(id);
            if (beneficiary_Types == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary_Types);
        }

        // POST: Beneficiary_Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beneficiary_Types beneficiary_Types = db.Beneficiary_Types.Find(id);
            db.Beneficiary_Types.Remove(beneficiary_Types);
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
