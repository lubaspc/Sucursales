using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sucursales.Models;

namespace Sucursales.Controllers
{
    [Authorize]
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            var applicationUsers = db.Users;
            return View(applicationUsers.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,UserName,Password")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid && registerViewModel.Password.Length > 0)
            {
                var applicationUser = new ApplicationUser { Email = registerViewModel.Email, UserName = registerViewModel.UserName};
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                userManager.Create(applicationUser, registerViewModel.Password);
                userManager.AddToRole(applicationUser.Id, "Administrador");
                
                return RedirectToAction("Index");
            }

            return View(registerViewModel);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            applicationUser.PasswordHash = "";
            RegisterViewModel registerViewModel = new RegisterViewModel
            {
                Id = id,
                Email = applicationUser.Email,
                UserName = applicationUser.UserName,

            };
            return View(registerViewModel);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName,Password")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser ApplicationUser = db.Users.Find(registerViewModel.Id);
                ApplicationUser.Email = registerViewModel.Email;
                ApplicationUser.UserName = registerViewModel.UserName;
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                userManager.Update(ApplicationUser);
               
                if(registerViewModel.Password != null && registerViewModel.Password.Length > 0)
                { 
                    userManager.RemovePassword(ApplicationUser.Id);
                    userManager.AddPassword(ApplicationUser.Id, registerViewModel.Password);
                }
                return RedirectToAction("Index");
            }
            return View(registerViewModel);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Upsert(String id)
        {
            ApplicationUser ApplicationUser = db.Users.Find(id);
            ApplicationUser.LockoutEnabled = !ApplicationUser.LockoutEnabled;
            if (ApplicationUser.LockoutEnabled)
            {
                ApplicationUser.LockoutEndDateUtc = DateTime.Now.AddYears(5);
            }
            else
            {
                ApplicationUser.LockoutEndDateUtc = null;
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            userManager.Update(ApplicationUser);
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
