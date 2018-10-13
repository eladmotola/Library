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
