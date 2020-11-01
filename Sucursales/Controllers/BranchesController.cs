using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sucursales.Models;

namespace Sucursales.Controllers
{
    [Authorize]
    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Branches
        public ActionResult Index(string address= "",string name = "")
        {
            var branch = db.Branch.Include(b => b.Address);
            if (address.Length > 0)
            {
                branch = branch.Where(b => b.Address.FullAddress.Contains(address));
            }

            if (name.Length > 0)
            {
                branch = branch.Where(b => b.Name.Contains(name));
            }
            return View(branch.ToList());
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Address, "Id", "Street");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                var Address = db.Address.Add(branch.Address);
                branch.AddressId = Address.Id;
                db.Branch.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch
                .Include(item => item.Address)
                .FirstOrDefault(item => item.Id == id);

            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,AddressId")] Branch branch)
        {
            if (ModelState.IsValid)
            {
              
                db.Entry(branch.Address).State = EntityState.Modified;
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        public ActionResult Upsert(int id)
        {
            Branch branch = db.Branch.Find(id);
            branch.Active = !branch.Active;
            db.Entry(branch).State = EntityState.Modified;
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
