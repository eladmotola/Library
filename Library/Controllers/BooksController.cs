using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Accord.MachineLearning.Rules;
using Accord.Statistics.Analysis;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Books
        public ActionResult Index()
        {
            var genres = new SelectList(db.Genres, "Name", "Name");
            List<SelectListItem> genresList = new List<SelectListItem>();
            genresList.Add(new SelectListItem { Text = "", Value = "" });
            foreach (var item in genres)
            {
                genresList.Add(item);
            }
            ViewBag.GenreId = genresList;
            return View(db.Books.Include(b => b.Genre).ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Where(x => x.Id == id).Include(x => x.Genre).FirstOrDefault();
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Author,ReleaseDate,GenreId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,ReleaseDate,GenreId")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (db.Books.AsNoTracking().SingleOrDefault(x => x.Id == book.Id) != null)
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Where(x => x.Id == id).Include(x => x.Genre).FirstOrDefault();
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string Id, string BookName, string Author, string GenreId, DateTime? ReleaseDate)
        {
            var genres = new SelectList(db.Genres, "Name", "Name");
            List<SelectListItem> genresList = new List<SelectListItem>();
            genresList.Add(new SelectListItem { Text = "", Value = "" });
            foreach (var item in genres)
            {
                genresList.Add(item);
            }
            ViewBag.GenreId = genresList;
            IEnumerable<Book> books = db.Books.Include(b => b.Genre);

            if (Id != string.Empty)
            {
                books = books.Where(book => book.Id.ToString() == Id);
            }

            if (BookName != string.Empty)
            {
                books = books.Where(book => book.Name.Contains(BookName));
            }

            if (Author != string.Empty)
            {
                books = books.Where(book => book.Author.Contains(Author));
            }

            if (GenreId != string.Empty)
            {
                books = books.Where(book => book.Genre.Name == GenreId);
            }

            if (ReleaseDate.HasValue)
            {
                books = books.Where(book => book.ReleaseDate.Equals(ReleaseDate));
            }

            return View("Index", books);
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
