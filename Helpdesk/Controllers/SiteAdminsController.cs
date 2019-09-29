using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helpdesk.Models;

namespace Helpdesk.Controllers
{
    public class SiteAdminsController : Controller
    {
        private HelpdeskDb _db = new HelpdeskDb();

        // GET: SiteAdmins
        public async Task<ActionResult> Index()
        {
            return View(await _db.Site_Admins.ToListAsync());
        }

        // GET: SiteAdmins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAdmins siteAdmins = await _db.Site_Admins.FindAsync(id);
            if (siteAdmins == null)
            {
                return HttpNotFound();
            }
            return View(siteAdmins);
        }

        // GET: SiteAdmins/Create
    // public ActionResult SiteAdmins()
    // {
    //     var model = from a in _db.Site_Admins orderby a.Name ascending select a;
    //     return View(model);
    // }
    //
    //
    //
    // // GET: Site_Admins/Details/5
    // public ActionResult Details(int id)
    // {
    //     var view = _db.Site_Admins.Single(v => v.Id == id);
    //     return View(view);
    // }
    //
         // GET: Site_Admins/Create
         public ActionResult Create()
         {
             return View("Create");
         }

        // POST: Site_Admins/Create
        [HttpPost]
        public ActionResult Create(SiteAdmins SiteAdmins)
        {
            if (ModelState.IsValid)
            {
                _db.Site_Admins.Add(SiteAdmins);
                _db.SaveChanges();

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            return View(SiteAdmins);

        }

        // GET: Site_Admins/Edit/5
        public ActionResult Edit(int id = 0)
        {
            SiteAdmins siteadmins = _db.Site_Admins.Find(id);
            if (siteadmins == null)
            {
                return HttpNotFound();
            }

            return View(siteadmins);
        }

        // POST: Admins/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SiteAdmins siteadmins)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(siteadmins).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("SiteAdmins");

            }
            return View(siteadmins);
        }


        // GET: Admins/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteAdmins admins = await _db.Site_Admins.FindAsync(id);
            if (admins == null)
            {
                return HttpNotFound();
            }
            return View(admins);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SiteAdmins admins = await _db.Site_Admins.FindAsync(id);
            _db.Site_Admins.Remove(admins);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
