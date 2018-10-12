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
    public class LoansController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Loans
        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.Book).Include(l => l.Customer);
            return View(loans.ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? BookId, int? CustomerId)
        {
            if (BookId == null || CustomerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(BookId, CustomerId);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "PersonalId");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,CustomerId,LoanDate,ReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                if (db.Loans.SingleOrDefault(x => x.CustomerId == loan.CustomerId && x.BookId == loan.BookId) == null)
                {
                    db.Loans.Add(loan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }

            ViewBag.BookId = new SelectList(db.Books, "Id", "Name", loan.BookId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "PersonalId", loan.CustomerId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? BookId, int? CustomerId)
        {
            if (BookId == null || CustomerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(BookId, CustomerId);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "Name", loan.BookId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", loan.CustomerId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,CustomerId,LoanDate,ReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "Name", loan.BookId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", loan.CustomerId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? BookId, int? CustomerId)
        {
            if (BookId == null || CustomerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(BookId, CustomerId);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int BookId, int CustomerId)
        {
            Loan loan = db.Loans.Find(BookId, CustomerId);
            db.Loans.Remove(loan);
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
