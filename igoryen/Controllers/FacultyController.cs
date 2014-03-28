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
  public class FacultyController : Controller {
    private DataContext db = new DataContext();

    //==================================================
    // Bring in namespaces
    //==================================================
    private Repo_Course rc = new Repo_Course();
    private Repo_Faculty rf = new Repo_Faculty();
    private Repo_Message rm = new Repo_Message();

    // C

    //======================================
    // Create() - GET: /Faculty/Create
    //======================================
    [Authorize(Roles = "Admin")]
    public ActionResult Create() {
      ViewBag.courses = rc.getCourseSelectList(); // 20
      ViewBag.messages = rm.getMessageSelectList(); // 30
      return View();
    }

    //======================================
    // Create() - POST: /Faculty/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //======================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(FormCollection form) {
      //try {
      if (form.Count == 6) { // 20
        rf.createFaculty(form[1], form[2], form[3], form[4], form[5]); // 30 messages shouldn't be there
      }
      return RedirectToAction("Index");
    }

    // D
    
    //======================================
    // Delete() - GET: /Faculty/Delete/5
    //======================================
    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int? id) {
      if (id == null) { // 10
        ViewBag.ExceptionMessage = "That was an invalid record";
        return View("Error");
      }
      var faculty = rf.getFacultyFullAM(id);
      if (faculty == null) {
        ViewBag.ExceptionMessage = "That record could not be deleted because it doesn't exist";
        return View("Error");
      }
      return View(faculty);
    }

    //======================================
    // Delete() - POST: /Faculty/Delete/5
    //======================================
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id) {
      // Course course = db.Courses.Find(id);
      // db.Courses.Remove(course);
      // db.SaveChanges();
      rf.DeleteFaculty(id);
      return RedirectToAction("Index");
    }

    //======================================
    // Details() - GET: /Faculty/Details/5
    //======================================
    [Authorize(Roles = "Admin")]
    public ActionResult Details(int? id) {
      return View(rf.getFacultyFull(id));
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
    // Edit() - GET: /Faculty/Edit/5
    //======================================
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(int? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Faculty faculty = await db.Faculties.FindAsync(id);
      if (faculty == null) {
        return HttpNotFound();
      }
      return View(faculty);
    }

    //======================================
    // Edit() - POST: /Faculty/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //======================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "PersonId,SenecaId,FirstName,LastName,Phone")] Faculty faculty) {
      if (ModelState.IsValid) {
        db.Entry(faculty).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View(faculty);
    }

    // I

    //======================================
    // Index() - GET: /Faculty/
    //======================================
    public ActionResult Index() {
      return View(rf.getListOfFacultyBaseAM());
    }

  }
}
