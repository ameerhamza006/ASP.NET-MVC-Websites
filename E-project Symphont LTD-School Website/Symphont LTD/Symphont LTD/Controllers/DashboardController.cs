using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Symphont_LTD.Models;

namespace Symphont_LTD.Controllers
{
    public class DashboardController : Controller
    {
        private Symphont_LTDEntities db = new Symphont_LTDEntities();

        // GET: Dashboard
        
        public ActionResult Index()
        {
            var registrations = db.Registrations.Include(r => r.Course);
            //return View(registrations.ToList());
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            ViewBag.C_id = new SelectList(db.Courses, "C_id", "Course_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,First_Name,Last_Name,Email,Password,Gender,Date_of_birth,phone,pichure,C_id,Country,Role")] Registration registration, HttpPostedFileBase p)
        {
            if (ModelState.IsValid)
            {
                string data = Path.Combine(Server.MapPath("~/picture/"), Path.GetFileName(p.FileName));
                
                registration.pichure = p.FileName;
                p.SaveAs(data);
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.C_id = new SelectList(db.Courses, "C_id", "Course_Name", registration.C_id);
            return View(registration);
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_id = new SelectList(db.Courses, "C_id", "Course_Name", registration.C_id);
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,First_Name,Last_Name,Email,Password,Gender,Date_of_birth,phone,pichure,C_id,Country,Role")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_id = new SelectList(db.Courses, "C_id", "Course_Name", registration.C_id);
            return View(registration);
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Registration r)
        {
            var a = db.Registrations.Where(x => x.Email == r.Email && x.Password == r.Password).FirstOrDefault();
            if (a != null)
            {
                FormsAuthentication.SetAuthCookie(r.Email,false);
                Session["name"] = a.First_Name;
                Session["role"] = a.Role;
                Session["img"] = a.pichure;
                return RedirectToAction("AdminDashboard", "Dashboard");
            }
            else
            {
                ViewBag.mesaage = "Email & password is invalid";
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult AdminDashboard()
        {
            return View();
        }
    }
}
