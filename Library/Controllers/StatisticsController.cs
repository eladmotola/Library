using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class StatisticsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            User authUser = db.Users.SingleOrDefault(user => user.Username == Username && user.Password == Password);

            if (authUser != null)
            {
                System.Web.HttpContext.Current.Session["LoggedIn"] = true;
                return RedirectToAction("/Index");
            }
            else
            {
                return View();
            }
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
