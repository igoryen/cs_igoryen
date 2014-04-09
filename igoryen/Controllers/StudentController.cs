using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using igoryen.Models;
using igoryen.ViewModels;

namespace igoryen.Controllers {

    [Authorize]
    public class StudentController : Controller {

        private DataContext db = new DataContext();

        //==================================================
        // Bring in namespaces
        //==================================================
        private Repo_Course rc = new Repo_Course();
        private Repo_ComMethod rcm = new Repo_ComMethod();
        private Repo_Student rs = new Repo_Student();

        // Action methods alphabetically

        // C

        //======================================
        // Create() - GET: /Student/Create
        // 20. display list of Courses on the view
        // 30. display list of ComMethods on the view
        //======================================
        [Authorize(Roles = "Admin")]
        public ActionResult Create() {
            ViewBag.courses = rc.getSelectListOfCourse(); // 20
            ViewBag.commethods = rcm.getComMethodSelectList(); // 30
            return View();
        }

        //======================================
        // Create() - POST: /Student/Create
        // 20. Change to 7 when ComMethod is clarified
        // 30. add ", form[6]" for ComMethod when it's clarified
        //======================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form) {
            //try {
            if (form.Count == 6) { // 20
                rs.createStudent(form[1], form[2], form[3], form[4], form[5]/*, form[6]*/); // 30
            }
            return RedirectToAction("Index");
            /*
            }
            catch (Exception e) {
              ViewBag.ExceptionMessage0 = "CourseController.cs/Create(): " + e.Message;
              ViewBag.ExceptionMessage1 = "form.Count: " + form.Count;
              return View("Error");
            }
             */
        }

        // D

        //==================================================
        // Delete() - GET: /Student/Delete/5
        // 10. if id == null, don't delete
        //==================================================
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id) {
            if (id == null) { // 10
                ViewBag.ExceptionMessage = "That was an invalid record";
                return View("Error");
            }
            var course = rs.getStudentFullAM(id);
            if (course == null) {
                ViewBag.ExceptionMessage = "That record could not be deleted because it doesn't exist";
                return View("Error");
            }
            return View(course);
        }

        //==================================================
        // Delete() - POST: /Student/Delete/5
        //==================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            // Course course = db.Courses.Find(id);
            // db.Courses.Remove(course);
            // db.SaveChanges();
            rs.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        //======================================
        // Details() - GET: /Student/Details/5
        //======================================
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id) {
            return View(rs.getStudentFull(id));
        }

        //======================================
        // Dispose()
        //======================================
        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // E

        //======================================
        // Edit() - GET: /Student/Edit/5
        //======================================
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null) {
                return HttpNotFound();
            }
            return View(student);
        }

        //======================================
        // Edit() - POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //======================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StudentNumber,FirstName,LastName,Phone")] Student student) {
            if (ModelState.IsValid) {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //======================================
        // Index() - GET: /Student/
        //======================================
        public ActionResult Index() {
            return View(rs.getListOfStudentBaseAM());
        }

    }
}
