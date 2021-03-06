﻿using System;
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
    public class CustomersController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            User authUser = db.Users.SingleOrDefault(user => user.Username == Username &&
                                                     user.Password == Password);

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

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonalID,FirstName,FamilyName,Email,PhoneNumber,Address,Gender,Birthday")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (db.Customers.Where(x => x.PersonalID == customer.PersonalID).ToList().Count != 0)
                {
                    return View();
                }
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonalID,FirstName,FamilyName,Email,PhoneNumber,Address,Gender,Birthday")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (db.Customers.AsNoTracking().SingleOrDefault(x => x.Id == customer.Id) != null)
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string PersonalID, string FirstName, string FamilyName, string Age)
        {
            IEnumerable<Customer> customers = db.Customers;

            if (PersonalID != string.Empty)
            {
                customers = customers.Where(customer => customer.PersonalID.ToString().ToUpper().Contains(PersonalID.ToUpper()));
            }

            if (FirstName != string.Empty)
            {
                customers = customers.Where(customer => customer.FirstName.ToUpper().Contains(FirstName.ToUpper()));
            }

            if (FamilyName != string.Empty)
            {
                customers = customers.Where(customer => customer.FamilyName.ToUpper().Contains(FamilyName.ToUpper()));
            }

            if (Age != string.Empty && int.TryParse(Age, out int age))
            {
                customers = customers.Where(customer => Math.Floor((double)DateTime.Now.Subtract(customer.Birthday).Days / 365) == age);
            }

            return View("Index", customers);
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
