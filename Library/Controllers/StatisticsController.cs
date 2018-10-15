using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using Newtonsoft.Json.Linq;

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
            User authUser = db.Users.SingleOrDefault(user => user.Username == Username && 
                                                     user.Password == Password && 
                                                     user.RoleId == 1);

            if (authUser != null)
            {
                System.Web.HttpContext.Current.Session["LoggedIn"] = authUser.RoleId;
                return RedirectToAction("/Index");
            }
            else
            {
                return View();
            }
        }

        // GET: Statistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(u => u.Username == user.Username).ToList().Count > 0)
                {
                    return View("Create");
                }
                user.RoleId = 2;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("/Index");
            }

            return View("Create");
        }

        public string getNumberOfBookLoans()
        {
            JArray returnObject = new JArray();

            var joined = from b in db.Books
                         join l in db.Loans on b.Id equals l.BookId
                         select new
                         {
                             bookName = b.Name,
                             loan = l
                         };
            var grouped = joined.GroupBy(res => res.bookName);

            foreach (var item in grouped)
            {
                JObject obj = new JObject();
                obj["Book"] = item.Key;
                obj["LoanCount"] = item.Count();
                returnObject.Add(obj);
            }

            string json = returnObject.ToString();

            return json;
        }

        public string getNumberOfCustomerLoans()
        {
            JArray returnObject = new JArray();

            var joined = from c in db.Customers
                         join l in db.Loans on c.Id equals l.CustomerId
                         select new
                         {
                             customerName = c.FirstName + " " + c.FamilyName,
                             loan = l
                         };
            var grouped = joined.GroupBy(res => res.customerName);

            foreach (var item in grouped)
            {
                JObject obj = new JObject();
                obj["Customer"] = item.Key;
                obj["LoanCount"] = item.Count();
                returnObject.Add(obj);
            }

            string json = returnObject.ToString();

            return json;
        }


        public string getBooksByGenre()
        {
            var groupedBooks = db.Books.GroupBy(book => book.Genre);
            JArray returnObject = new JArray();

            foreach (var item in groupedBooks)
            {
                JObject obj = new JObject();
                obj["Genre"] = item.Key.Name;
                obj["BookCount"] = item.Count();
                returnObject.Add(obj);
            }

            string json = returnObject.ToString();

            return json;
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
