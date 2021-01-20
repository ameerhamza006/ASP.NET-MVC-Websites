using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Symphont_LTD.Models;

namespace Symphont_LTD.Controllers
{
    public class FeesController : Controller
    {
        private Symphont_LTDEntities db = new Symphont_LTDEntities();

        // GET: Fees
        public ActionResult Index()
        {
            var fees = db.Fees.Include(f => f.Registration);
            return View(fees.ToList());
        }

        // GET: Fees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: Fees/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Registrations, "ID", "First_Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,F_id,Amount,status,Pay_Date")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Fees.Add(fee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Registrations, "ID", "First_Name", fee.ID);
            return View(fee);
        }

        // GET: Fees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Registrations, "ID", "First_Name", fee.ID);
            return View(fee);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,F_id,Amount,status,Pay_Date")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Registrations, "ID", "First_Name", fee.ID);
            return View(fee);
        }

        // GET: Fees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fee fee = db.Fees.Find(id);
            db.Fees.Remove(fee);
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
