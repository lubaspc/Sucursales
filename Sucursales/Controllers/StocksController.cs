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
    public class StocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stocks
        public ActionResult Index(string branch = "",string product = "",string quantity = "")
        {
            var stock = db.Stock.Include(s => s.Branch).Include(s => s.Product);
            if(branch.Length > 0)
            {
                stock = stock.Where(s => s.Branch.Name.Contains(branch));
            }
            if (product.Length > 0)
            {
                stock = stock.Where(s => s.Product.Name.Contains(product));
            }
            if (quantity.Length > 0)
            {
                stock = stock.Where(s => s.Amount.ToString().Contains(quantity));
            }
            return View(stock.ToList());
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock
                .Include(s => s.StockHistories)
                .Include(s => s.Product)
                .Include(s => s.Branch)
                .FirstOrDefault(s=> s.Id==id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branch, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BranchId,ProductId,Amount")] Stock stock)
        {

            if (ModelState.IsValid)
            {
                if(db.Stock.Where(s => s.BranchId == stock.BranchId)
                    .Where(s => s.ProductId == stock.ProductId)
                    .FirstOrDefault() != null)
                {
                    ViewBag.error = "El producto ya esta registrado en esta sucursal";
                }
                else
                {
                    db.Stock.Add(stock);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }

            ViewBag.BranchId = new SelectList(db.Branch, "Id", "Name", stock.BranchId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", stock.ProductId);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(db.Branch, "Id", "Name", stock.BranchId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", stock.ProductId);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BranchId,ProductId,Amount")] Stock stock,int OldAmount)
        {
            if (ModelState.IsValid)
            {
                var dif = stock.Amount - OldAmount ;
                var entry = true;
                if(dif <= 0)
                {
                    entry = false;
                    dif *= -1;
                }
                db.StockHistory.Add(new StockHistory
                {
                    StockId = stock.Id,
                    Amounth = dif,
                    Entity = entry,
                    Created = DateTime.Now
                });
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchId = new SelectList(db.Branch, "Id", "Name", stock.BranchId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", stock.ProductId);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            Stock stock = db.Stock.Find(id);
            db.Stock.Remove(stock);
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
