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

namespace igoryen.Controllers {
  public class StudentController : Controller {

    private DataContext db = new DataContext();

    // Action methods alphabetically

    // C

    //======================================
    // Create() - GET: /Student/Create
    //======================================
    public ActionResult Create() {
      return View();
    }

    //======================================
    // Create() - POST: /Student/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //======================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Id,StudentNumber,FirstName,LastName,Phone")] Student student) {
      if (ModelState.IsValid) {
        db.Students.Add(student);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      return View(student);
    }

    // D
    
    //======================================
    // Delete() - GET: /Student/Delete/5
    //======================================
    public async Task<ActionResult> Delete(int? id) {
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
    // DeleteConfirmed() - POST: /Student/Delete/5
    //======================================
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id) {
      Student student = await db.Students.FindAsync(id);
      db.Students.Remove(student);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    //======================================
    // Details() - GET: /Student/Details/5
    //======================================
    public async Task<ActionResult> Details(int? id) {
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
    public async Task<ActionResult> Index() {
      return View(await db.Students.ToListAsync());
    }

  }
}
