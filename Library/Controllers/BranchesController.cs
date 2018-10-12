using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Library.Models;

namespace Library.Controllers
{
    public class BranchesController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Branches
        public ActionResult Index()
        {
            return View();
        }

        public string GetBranches()
        {
            List<Branch> branch = db.Branches.ToList();
            if (branch == null)
            {
                return "";
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(branch);

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
