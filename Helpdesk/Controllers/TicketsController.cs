using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Sql;
using Helpdesk.Models;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections;

namespace Helpdesk.Controllers
{
    public class TicketsController : Controller
    {
        private HelpdeskDb _db = new HelpdeskDb();

        // GET: Tickets
        public ActionResult Index(string searchTerm, int? page, string Priorities,
            string Categories, string Site_Admins, string Name, string Location, string Region,
            string Subject)
        {
            List<Tickets> List = new List<Tickets>();
            var model = from a in _db.Tickets
                        select a;

            // Filtered Search Variables
            var PriorityLst = new List<string>();
            var PriorityQry = from d in _db.Tickets
                              orderby d.Priorities
                              select d.Priorities;

            PriorityLst.AddRange(PriorityQry.Distinct());
            ViewBag.Priorities = new SelectList(PriorityLst);

            var CategoryLst = new List<string>();
            var CategoryQry = from e in _db.Tickets
                              orderby e.Categories
                              select e.Categories;

            CategoryLst.AddRange(CategoryQry.Distinct());
            ViewBag.Categories = new SelectList(CategoryLst);

            var AssignLst = new List<string>();
            var AssignQry = from f in _db.Tickets
                            orderby f.Site_Admins
                            select f.Site_Admins;

            AssignLst.AddRange(AssignQry.Distinct());
            ViewBag.Site_Admins = new SelectList(AssignLst);

            var NameLst = new List<string>();
            var NameyQry = from g in _db.Tickets
                           orderby g.Name
                           select g.Name;

            NameLst.AddRange(NameyQry.Distinct());
            ViewBag.Name = new SelectList(NameLst);

            var LocationLst = new List<string>();
            var LocationQry = from h in _db.Tickets
                              orderby h.Location
                              select h.Location;

            LocationLst.AddRange(LocationQry.Distinct());
            ViewBag.Location = new SelectList(LocationLst);

            var RegionLst = new List<string>();
            var RegionQry = from j in _db.Tickets
                            orderby j.Region
                            select j.Region;

            RegionLst.AddRange(RegionQry.Distinct());
            ViewBag.Region = new SelectList(RegionLst);

            //  var SubjectLst = new List<string>();
            //  var SubjectQry = from k in _db.Tickets
            //                  orderby k.Subject
            //                  select k.Subject;
            //
            //  SubjectLst.AddRange(SubjectQry.Distinct());
            //  ViewBag.Subject = new SelectList(SubjectLst);


            //Enables Filtered Search
            if (!String.IsNullOrEmpty(searchTerm))
            {
                Int32.TryParse(searchTerm, out int ticketid);
                model = model.Where(a => a.TicketsId == ticketid //);
                // To Enable Search All of Tickets DB
                // || a.Name.StartsWith(searchTerm)
                // || a.Site_Admins.StartsWith(searchTerm)
                // || a.ComputerName.StartsWith(searchTerm)
                 || a.Subject.Contains(searchTerm));
                // || a.Region.StartsWith(searchTerm)
                // || a.Location.StartsWith(searchTerm)
                // || a.Priorities.StartsWith(searchTerm)
                // || a.Categories.StartsWith(searchTerm));
            }
            if (!String.IsNullOrEmpty(Priorities))
            {

                model = model.Where(d => d.Priorities.StartsWith(Priorities));
            }
            if (!String.IsNullOrEmpty(Categories))
            {

                model = model.Where(e => e.Categories.StartsWith(Categories));
            }
            if (!String.IsNullOrEmpty(Site_Admins))
            {

                model = model.Where(f => f.Site_Admins.StartsWith(Site_Admins));
            }
            if (!String.IsNullOrEmpty(Name))
            {

                model = model.Where(g => g.Name.StartsWith(Name));
            }
            if (!String.IsNullOrEmpty(Location))
            {

                model = model.Where(h => h.Location.StartsWith(Location));
            }
            if (!String.IsNullOrEmpty(Region))
            {

                model = model.Where(j => j.Region.StartsWith(Region));
            }
            if (!String.IsNullOrEmpty(Subject))
            {

                model = model.Where(k => k.Subject.StartsWith(Subject));
            }
            Response.AppendHeader("Refresh", "120");
            model = model.OrderByDescending(a => a.Date);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }


        // GET: Tickets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = await _db.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // GET: Tickets/Create
        public void PopulateLists(Tickets model)
        {
            ViewData["Categories"] = new SelectList(_db.Categories, "CategoryName", "CategoryName", model.Categories);

            ViewData["Priorities"] = new SelectList(_db.Priorities, "PriorityName", "PriorityName", model.Priorities);

            ViewData["Site_Admins"] = new SelectList(_db.Site_Admins, "Name", "Name", model.Site_Admins);

            ViewData["Location"] = new SelectList(_db.Location, "Name", "Name", model.Location);

            ViewData["Region"] = new SelectList(_db.Region, "Name", "Name", model.Region);
        }

        // GET: Tickets
        // public ActionResult Tickets(Tickets tickets, string name)
        // {
        //
        //     var model = from a in _db.Tickets
        //                 orderby a.Date ascending
        //                 select a;
        //     return View(model);
        // }
        //
        // // GET: Tickets/Details/5
        // public ActionResult Details(int id)
        // {
        //     var view = _db.Tickets.Single(v => v.TicketsId == id);
        //     return View(view);
        // }

        // GET: Tickets/Create
        public ActionResult Create()
        {

            var model = new Tickets();
            PopulateLists(model);


            return View("Create");
        }
        //Save files to database here//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tickets model)
        {
            if (ModelState.IsValid)
            {
                List<FileDetails> fileDetails = new List<FileDetails>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetails fileDetail = new FileDetails()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }
                model.FileDetails = fileDetails;
                _db.Tickets.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }



        public ActionResult Edit(int id = 0)
        {
            Tickets model = _db.Tickets.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            PopulateLists(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int? id, Tickets model)
        {
            if (ModelState.IsValid)
            {

                _db.Entry(model).State = EntityState.Modified;
                PopulateLists(model);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Details(Tickets model)

        {

            if (ModelState.IsValid)

            {



                //New Files

                for (int i = 0; i < Request.Files.Count; i++)

                {

                    var file = Request.Files[i];



                    if (file != null && file.ContentLength > 0)

                    {

                        var fileName = Path.GetFileName(file.FileName);

                        FileDetails fileDetail = new FileDetails()

                        {

                            FileName = fileName,

                            Extension = Path.GetExtension(fileName),

                            Id = Guid.NewGuid(),

                            TicketsId = model.TicketsId

                        };

                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);

                        file.SaveAs(path);



                        _db.Entry(fileDetail).State = EntityState.Added;

                    }

                }



                _db.Entry(model).State = EntityState.Modified;

                _db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(model);

        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int id)
        {
            Tickets tickets = _db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleteconfirmed(int id = 0)
        {
            Tickets tickets = _db.Tickets.Find(id);
            _db.Tickets.Remove(tickets);
            _db.SaveChanges();
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }
        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }


    
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}

