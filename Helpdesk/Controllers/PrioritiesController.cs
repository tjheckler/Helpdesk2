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
    public class PrioritiesController : Controller
    {
        private HelpdeskDb _db = new HelpdeskDb();

        // GET: Priorities
        public async Task<ActionResult> Index()
        {
            return View(await _db.Priorities.ToListAsync());
        }

        // GET: Priorities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priorities priorities = await _db.Priorities.FindAsync(id);
            if (priorities == null)
            {
                return HttpNotFound();
            }
            return View(priorities);
        }

    // // GET: Priorities/Create
    // public ActionResult Priorities()
    // {
    //     var model = from a in _db.Priorities orderby a.PriorityName ascending select a;
    //     return View(model);
    // }


   //  // GET: Category/Details/5
   //  public ActionResult Details(int id)
   //  {
   //      var view = _db.Priorities.Single(v => v.Id == id);
   //      return View(view);
   //  }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Priorities Priority)
        {
            if (ModelState.IsValid)
            {
                _db.Priorities.Add(Priority);
                _db.SaveChanges();

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            return View(Priority);
        }


        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Priorities Priority = _db.Priorities.Find(id);
            if (Priority == null)
            {
                return HttpNotFound();
            }

            return View(Priority);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Priorities Priority)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(Priority).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Priorities");

            }
            return View(Priority);
        }


        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priorities Priority = await _db.Priorities.FindAsync(id);
            if (Priority == null)
            {
                return HttpNotFound();
            }
            return View(Priority);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Priorities Priority = await _db.Priorities.FindAsync(id);
            _db.Priorities.Remove(Priority);
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
