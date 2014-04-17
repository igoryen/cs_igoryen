using igoryen.Models;
using igoryen.ViewModels;
using Microsoft.AspNet.Identity; // 10
using Microsoft.AspNet.Identity.EntityFramework; // 20
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net; // 45
using System.Threading.Tasks; // 50
using System.Web;
using System.Web.Mvc;


namespace igoryen.Controllers {

    [Authorize]
    public class CancellationsController : Controller {

        private DataContext dc;
        private UserManager<ApplicationUser> manager;
        //private UserManager<IdentityUser> manager;
        private Repo_Course rc = new Repo_Course();
        private Repo_Cancellation rcc = new Repo_Cancellation();


        [Authorize(Roles = "Admin")] // 10
        public ActionResult All() {
            return View(dc.Cancellations.ToList());
        }


        public CancellationsController() {
            dc = new DataContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dc));
        }


        // GET: /Cancellations/Create
        public ActionResult Create() {
            var currentuser = manager.FindById(User.Identity.GetUserId());

            ViewBag.courses = rc.getSelectListOfCourse(currentuser.Id);
            return View();
        }

        // POST: /Cancellation/Create
        [HttpPost]
        [Authorize(Roles = "Faculty")]
        public ActionResult Create(FormCollection form) {
            try {
                if (form.Count == 4) {
                    rcc.createCancellation(form[1], form[2], form[3]);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                ViewBag.ExceptionMessage1 = e.Message;
                return View("Error");
            }
        }

        // GET: /Cancellations/Delete/5
        public ActionResult Delete(int? id) {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (id == null) {
                ViewBag.ExceptionMessage = "That was an invalid record";
                return View("Error");
            }
            Cancellation cancellation = dc.Cancellations.Find(id);
            //var cancellation = rcc.getCancellationFullAM(id);
            if (cancellation == null) {
                ViewBag.ExceptionMessage = "That record could not be deleted because it doesn't exist";
                return View("Error");
            }
            if (cancellation.User.Id != currentUser.Id) {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(cancellation);
        }


        // POST: /Cancellations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Cancellation cancellation = await dc.Cancellations.FindAsync(id);
            dc.Cancellations.Remove(cancellation);
            await dc.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int? id) {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null) {
                var errors = new ViewModels.VM_Error();
                errors.ErrorMessages["ExceptionMessage"] = "No id specified";
                return View("Error", errors); // 12
            }
            Cancellation cancellation = await dc.Cancellations.FindAsync(id);
            if (cancellation == null) {
                return HttpNotFound();
            }
            if (cancellation.User.Id != currentUser.Id) {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(cancellation);
        }


        protected override void Dispose(bool disposing) {
            if (disposing) {
                dc.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: /Cancellations/Edit/5
        public async Task<ActionResult> Edit(int? id) {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (id == null) {
                ViewBag.ExceptionMessage = "That was an invalid record";
                return View("Error");
            }
            Cancellation cancellation = await dc.Cancellations.FindAsync(id);
            //var cancellation = rcc.getCancellationFullAM(id);
            if (cancellation == null) {
                ViewBag.ExceptionMessage = "That record could not be edited because it doesn't exist";
                return View("Error");
            }
            if (cancellation.User.Id != currentUser.Id) {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(cancellation);
        }

        // POST: /Cancellations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Message")] Cancellation cancellation) {
            if (ModelState.IsValid) {
                dc.Entry(cancellation).State = EntityState.Modified;
                await dc.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cancellation);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Find() {
            return View();
        }

        // GET: /Cancellations/Find/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Find(Cancellation cancellation) {
            string userName1 = cancellation.User.UserName;
            if (userName1 == null) {
                ViewBag.ExceptionMessage0 = "GET: Find() passed to POST: Find() this value: >>" + userName1 + "<<";
                return View("Error");
            }
            return RedirectToAction("Found", new { userName2 = userName1 });
        }


        public ActionResult Found(string userName2) {
            //ViewBag.debug0 = "userName4Search: >>" + userName2 + "<<";
            return View(dc.Cancellations.ToList().Where(Cancellation => Cancellation.User.UserName == userName2));
        }

        // GET: /Cancellations/
        public ActionResult Index() {
            string userId = User.Identity.GetUserId();
            var currentUser = manager.FindById(userId);
            if (currentUser == null) {
                ViewBag.ExceptionMessage5 = "CancellationsController.cs/Index()/currentUser: null";
                return View("Error");
            }
            return View(dc.Cancellations.ToList().Where(cancellation => cancellation.User.Id == currentUser.Id));
        }

    }
}
