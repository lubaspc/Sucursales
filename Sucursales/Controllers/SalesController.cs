﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Sucursales.Models;

namespace Sucursales.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sales
        public ActionResult Index(string branch = "",string total = "",string created = "",string username = "")
        {
            var sale = db.Sale
                .Include(s => s.User)
                .Include(s => s.Branch);
            if (branch.Length > 0)
            {
                sale = sale.Where(p => p.Branch.Name.Contains(branch));
            }
            if (total.Length > 0)
            {
                sale = sale.Where(p => p.Total.ToString().Contains(total));
            }
            if (created.Length > 0)
            {
                sale = sale.Where(p => p.Created.ToString().Contains(created));
            }
            if (username.Length > 0)
            {
                sale = sale.Where(p => p.User.UserName.Contains(username));
            }
            return View(sale.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale
                 .Include(s => s.User)
                .Include(s => s.Branch)
                .Include("saleProducts.Product")
                .FirstOrDefault(s => s.Id == id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branch, "Id", "Name");
            ViewBag.Products = db.Product.ToList();
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs("POST")]
        public ActionResult Create([Bind(Include = "BranchId")] Sale sale,SalesProduct[] Products)
        {
           if (ModelState.IsValid)
            {
                List<int> listIds = new List<int>();
                Stock stock = null;
                bool next = true;
                foreach(var saleProduct in Products)
                {
                    listIds.Add(saleProduct.ProductId);
                    stock = db.Stock.Where(s => s.ProductId == saleProduct.ProductId)
                                  .Where(s => s.BranchId == sale.BranchId)
                                  .FirstOrDefault();
                    if (stock == null || stock.Amount < saleProduct.Quantity)
                    {
                        ViewBag.Error = "La sucursal no cuenta con la cantidad para el producto "+ saleProduct.ProductId;
                        next = false;
                        break;
                    }
                }
                if (next)
                {


                    var products = db.Product.Where(p => listIds.Contains(p.Id)).ToList();
                    double total = 0.0;
                    products.ForEach(p =>
                    {
                        var saleProduct = Products.FirstOrDefault(sp => sp.ProductId == p.Id);
                        saleProduct.SubTotal = saleProduct.Quantity * p.Price;
                        total += saleProduct.SubTotal;

                        stock.Amount -= saleProduct.Quantity;
                        db.StockHistory.Add(new StockHistory
                        {
                            StockId = stock.Id,
                            Amounth = saleProduct.Quantity,
                            Entity = false,
                            Created = DateTime.Now
                        });
                        db.Entry(stock).State = EntityState.Modified;
                        db.SaveChanges();
                    });
                    sale.UserId = User.Identity.GetUserId();
                    sale.Created = DateTime.Now;
                    sale.Total = total;

                    db.Sale.Add(sale);
                    foreach (var saleProduct in Products)
                    {
                        saleProduct.SaleId = sale.Id;
                    }
                    db.SalesProduct.AddRange(Products);


                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Products = db.Product.ToList();
            ViewBag.BranchId = new SelectList(db.Branch, "Id", "Name", sale.BranchId);
            return View(sale);
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
