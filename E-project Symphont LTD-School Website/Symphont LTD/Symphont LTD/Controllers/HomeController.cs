using Symphont_LTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Symphont_LTD.Controllers
{
    public class HomeController : Controller
    {
        Symphont_LTDEntities db = new Symphont_LTDEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Course()
        {
            return View(db.Courses.ToList());
        }
        public ActionResult Coursedetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        public ActionResult faqs()
        {
            return View(db.FAQS.ToList());
        }
        public ActionResult contactus()
        {
            return View();
        }
        public ActionResult aboutus()
        {
            return View();
        }
    }
}