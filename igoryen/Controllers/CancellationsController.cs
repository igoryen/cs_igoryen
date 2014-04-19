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
using System.Data.Entity.Validation;


namespace igoryen.Controllers {

    [Authorize]
    public class CancellationsController : Controller {

        private DataContext dc;
        private UserManager<ApplicationUser> manager;
        //private UserManager<IdentityUser> manager;
        private Repo_Course rc = new Repo_Course();
        private Repo_Cancellation rcc = new Repo_Cancellation();
        private ViewModels.VM_Error vme = new ViewModels.VM_Error();
        static ViewModels.CancellationCreateForHttpGet cancellationToCreate = new CancellationCreateForHttpGet();

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
            var currentuser = manager.FindById(User.Identity.GetUserId()); // 13

            //if (currentuser != null) {
            //    var errors = new ViewModels.VM_Error();
            //    errors.ErrorMessages["ExceptionMessage"] = "currentUser = " + currentuser + " courses";
            //    return View("Error", errors); // 12
            //}

            var courses = rc.getSelectListOfCourse(currentuser.Id); // 14

            if (courses == null) {
                var errors = new ViewModels.VM_Error();
                errors.ErrorMessages["ExceptionMessage"] = 
                    "rc.getSelectListOfCourse(currentuser.Id) returned null";
                return View("Error", errors); // 12
            }
            //if (courses != null) {
            //    var errors = new ViewModels.VM_Error();
            //    errors.ErrorMessages["ExceptionMessage"] =
            //        "rc.getSelectListOfCourse(currentuser.Id) returned >>"+courses.Count() +"<< courses";
            //    return View("Error", errors); // 12
            //}
            cancellationToCreate.CourseSelectList = rc.getCourseSelectList(currentuser.Id); // 46

            return View(cancellationToCreate);
        }

        // POST: /Cancellation/Create
        [HttpPost]
        public ActionResult Create(ViewModels.CancellationCreateForHttpPost newItem) { // 51

            var currentuser = manager.FindById(User.Identity.GetUserId());

            if (ModelState.IsValid && newItem.CourseId != -1) { // 39 
                var cancellation = rcc.createCancellation(newItem); // 52
                cancellation.User = currentuser; //<=========================!!!!                
                //--------------------------------------------------
                dc.Cancellations.Add(cancellation); // 53
                try {
                    dc.SaveChanges();
                }
                catch (DbEntityValidationException e) {
                    //----------------------------------------------------------
                    List<string> output1 = new List<string>();
                    List<string> output2 = new List<string>();
                    foreach (var eve in e.EntityValidationErrors) {
                        output1.Add("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:");
                        foreach (var ve in eve.ValidationErrors) {
                            output1.Add("- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        } // foreach()

                        /*
                        Console.WriteLine("======================================");
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors) {
                          Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                              ve.PropertyName, ve.ErrorMessage);
                        }
                         */
                    } // foreach
                    output2 = output1;
                    throw;
                } // catch

                var createdCancellationFull = rcc.getCancellationFull(cancellation.CancellationId);
             
                //--------------------------------------------------
                //var createdCancellationFull = getCancellationFull(cancellation.CancellationId);

                if (createdCancellationFull == null) {
                    return View("Error", vme.GetErrorModel(null, ModelState));
                }
                else {
                    cancellationToCreate.Clear();
                    return RedirectToAction("Details", new { CancellationId = createdCancellationFull.CancellationId });
                }
            }
            else {
                if (newItem.CourseId == -1) 
                    ModelState.AddModelError("CourseSelectList", "Select a Course");
                //if (newItem.GenreId == null) ModelState.AddModelError("GenreSelectList", "Select One or More Genres");

                cancellationToCreate.Date = newItem.Date;
                cancellationToCreate.Message = newItem.Message;

                return View(cancellationToCreate);
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

        // Cancellation/Details
        // Details()
        public ActionResult Details(int? CancellationId) {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (CancellationId == null) {
                var errors = new ViewModels.VM_Error();
                errors.ErrorMessages["ExceptionMessage"] = "No CancellationId specified";
                return View("Error", errors); // 12
            }
            Cancellation cancellation = dc.Cancellations.Include("CourseBase").SingleOrDefault(cc => cc.CancellationId == CancellationId);
            if (cancellation == null) {
                return HttpNotFound();
            }
            if (cancellation.User.Id != currentUser.Id) {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            CancellationFull ccf = new CancellationFull();
            ccf.CancellationId = cancellation.CancellationId;
            ccf.CourseBase = cancellation.CourseBase;
            ccf.Date = cancellation.Date;

            return View(ccf);
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
        // Index()
        public ActionResult Index() {
            string userId = User.Identity.GetUserId();
            var currentUser = manager.FindById(userId);
            if (currentUser == null) {
                ViewBag.ExceptionMessage5 = "CancellationsController.cs/Index()/currentUser: null";
                return View("Error");
            }
            //---------------------------------------------------------
            // checking the role
            //if (User.IsInRole("Student")) {
            //    var errors = new ViewModels.VM_Error();
            //    errors.ErrorMessages["ExceptionMessage"] = "Hello, student " + currentUser.MyUserInfo.FirstName;
            //    return View("Error", errors); // 12
                //--- Option 1 ------------------------------------------

                //currentUser.Id.
                //for (int cc = 0; cc < dc.Cancellations.Count(); cc++){ // 10
                //    dc.Cancellations.ElementAt(cc).CourseBase.

                //}
                //List<Course> crss = new List<Course>(); // 20
                //for (int c = 0; c < dc.Courses.Count(); c++) { // 30
                //    for (int s = 0; s < dc.Courses.ElementAt(c).Students.Count(); s++) { // 40
                //        for (int cs = 0; cs < dc.Courses.ElementAt(c).Students.ElementAt(s).Courses.Count(); cs ++){ // 50
                //            if(dc.Courses.ElementAt(c).Students.ElementAt(s).Courses.ElementAt(cs).User.Id == currentUser.Id){
                //                crss.Add(dc.Courses.ElementAt(c).Students.ElementAt(s).Courses.ElementAt(cs));
                //            }
                            
                //        }

                //    }
                //}
                // 10. for each of all the `Cancellation` objects
                // 20. for all the courses which the currentUser (Student) is registered in.
                // 30. for each of all the `Course` objects
                // 40. for each of the `Course`s `Student` objects
                // 50. for each of the Course's Student's `Course` objects
                //-------------------------------------------------------
                   
            //}
            //else {
                return View(rcc.getListOfCancellationFull(currentUser.Id));
            //}
            //---------------------------------------------------------
            //return View(dc.Cancellations.Where(cancellation => cancellation.User.Id == currentUser.Id));
            //return View(rcc.getListOfCancellationFull(currentUser.Id));
        }

    }
}
