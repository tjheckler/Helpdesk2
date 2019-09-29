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
    public class CategoriesController : Controller
    {
        private HelpdeskDb _db = new HelpdeskDb();

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            return View(await _db.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await _db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
      //public ActionResult Categories()
      //{
      //    var model = from a in _db.Categories orderby a.CategoryName ascending select a;
      //    return View(model);
      //}
      //
      //
      ////GET: Category/Details/5
      //public ActionResult Details(int id)
      //{
      //    var view = _db.Categories.Single(v => v.Id == id);
      //    return View(view);
      //}

        // GET: Category/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Categories Category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            return View(Category);
        }


        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Categories category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Categories Category)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(Category).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Categories");

            }
            return View(Category);
        }


        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories Category = await _db.Categories.FindAsync(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(Category);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categories Category = await _db.Categories.FindAsync(id);
            _db.Categories.Remove(Category);
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