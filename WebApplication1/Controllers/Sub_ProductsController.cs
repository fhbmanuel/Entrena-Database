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
    public class Sub_ProductsController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        // GET: Sub_Products
        public ActionResult Index()
        {
            var sub_Products = db.Sub_Products.Include(s => s.Product);
            return View(sub_Products.ToList());
        }

        // GET: Sub_Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Products sub_Products = db.Sub_Products.Find(id);
            if (sub_Products == null)
            {
                return HttpNotFound();
            }
            return View(sub_Products);
        }

        // GET: Sub_Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "Product_ID", "Product_Name");
            return View();
        }

        // POST: Sub_Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sub_Product_ID,ProductID,Sub_Product_Name,Sub_Product_Duration,Sub_Product_Details")] Sub_Products sub_Products)
        {
            if (ModelState.IsValid)
            {
                db.Sub_Products.Add(sub_Products);
                db.SaveChanges();
                return RedirectToAction("Create","Sub_Products");
            }

            ViewBag.ProductID = new SelectList(db.Products, "Product_ID", "Product_Name", sub_Products.ProductID);
            return View(sub_Products);
        }

        // GET: Sub_Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Products sub_Products = db.Sub_Products.Find(id);
            if (sub_Products == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "Product_ID", "Product_Name", sub_Products.ProductID);
            return View(sub_Products);
        }

        // POST: Sub_Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sub_Product_ID,ProductID,Sub_Product_Name,Sub_Product_Duration,Sub_Product_Details")] Sub_Products sub_Products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_Products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "Product_ID", "Product_Name", sub_Products.ProductID);
            return View(sub_Products);
        }

        // GET: Sub_Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Products sub_Products = db.Sub_Products.Find(id);
            if (sub_Products == null)
            {
                return HttpNotFound();
            }
            return View(sub_Products);
        }

        // POST: Sub_Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_Products sub_Products = db.Sub_Products.Find(id);
            db.Sub_Products.Remove(sub_Products);
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
