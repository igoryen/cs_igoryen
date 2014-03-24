// 10. defines UserManager, RoleManager
// 20. identity role
// 45. for HttpStatusCode
// 50. for Task<>
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

// 10. Lock the whole route (controller) except for authorized
namespace igoryen.Controllers {

  [Authorize] // 10
  public class CancellationsController : Controller {

    //===================================================
    // Create namespaces
    //===================================================
    private DataContext db = new DataContext();
    private UserManager<ApplicationUser> manager;

    //==================================================
    // Bring in namespaces
    //==================================================
    private Repo_Course rc = new Repo_Course();
    private Repo_Cancellation rcc = new Repo_Cancellation();
    private Repo_Student rs = new Repo_Student();
    private Repo_Faculty rf = new Repo_Faculty();
    private Repo_Message rm = new Repo_Message();

    // Methods alphabetically

    // A

    //===================================================
    // All() - GET: /Cancellations/All
    // 10. lock this route for all but Admin (Igor/123456)
    //===================================================
    [Authorize(Roles = "Admin")] // 10
    public async Task<ActionResult> All() {
      return View(await db.Cancellations.ToListAsync());
    }

    // C

    //===================================================
    // CancellationsController() - contstructor
    //===================================================
    public CancellationsController() {
      db = new DataContext();
      manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
    }

    //===================================================
    // Create() - GET: /Cancellations/Create
    //===================================================
    public ActionResult Create() {
      return View();
    }

    //===================================================
    // Create() - POST: /Cancellations/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    // 20. Include = "Id, Message", but not "User"
    // 40. add operator "await" to make the method run asynchronously
    //      i.e. to await non-blocking API calls
    //===================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Id,Message")] Cancellation cancellation) { // 20
      var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
      if (ModelState.IsValid) {
        //cancellation.User = currentUser;
        db.Cancellations.Add(cancellation);
        // db.SaveChanges();
        await db.SaveChangesAsync(); // 40
        return RedirectToAction("Index");
      }

      return View(cancellation);
    }

    //===================================================
    // Delete() - GET: /Cancellations/Delete/5
    //===================================================
    public async Task<ActionResult> Delete(int? id) {
      var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
      if (id == null) {
        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ViewBag.ExceptionMessage = "That was an invalid record";
        return View("Error");
      }
      //Cancellation cancellation = await db.Cancellations.FindAsync(id);
      var cancellation = rcc.getCancellationFullAM(id);
      if (cancellation == null) {
        // return HttpNotFound();
        ViewBag.ExceptionMessage = "That record could not be deleted because it doesn't exist";
        return View("Error");
      }
      return View(cancellation);
    }

    // D

    //===================================================
    // Delete() - POST: /Cancellations/Delete/5
    //===================================================
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id) {
      Cancellation cancellation = await db.Cancellations.FindAsync(id);
      db.Cancellations.Remove(cancellation);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    //===================================================
    // Details() - GET: /Cancellations/Details/5
    //===================================================
    public ActionResult Details(int? id) {
      /*var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Cancellation cancellation = await db.Cancellations.FindAsync(id);

      if (cancellation == null) {
        return HttpNotFound();
      }
      if (cancellation.User.Id != currentUser.Id) {
        return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
      }
      */
      return View(rcc.getCancellationFullAM(id));
    }

    //===================================================
    // Dispose()
    //===================================================
    protected override void Dispose(bool disposing) {
      if (disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    // E

    //===================================================
    // Edit() - GET: /Cancellations/Edit/5
    //===================================================
    public ActionResult Edit(int? id) {
      //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
      if (id == null) {
        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ViewBag.ExceptionMessage = "That was an invalid record";
        return View("Error");
      }
      // Cancellation cancellation = await db.Cancellations.FindAsync(id);
      var cancellation = rcc.getCancellationFullAM(id);
      if (cancellation == null) {
        //return HttpNotFound();
        ViewBag.ExceptionMessage = "That record could not be edited because it doesn't exist";
        return View("Error");
      }
      //if (cancellation.User.Id != currentUser.Id) {
        // return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
      //}
      return View(cancellation);
    }

    //===================================================
    // Edit() - POST: /Cancellations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //===================================================
    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Message")] CancellationFull editItem) {
      if (ModelState.IsValid) {
        var newItem = rcc.editCancellationAM(editItem);
        if (newItem == null) {
          ViewBag.ExceptionMessage = "Record " + editItem.CancellationId + " was not found.";
          return View("Error");
        }
        else {
          return RedirectToAction("Index");
        }
        // db.Entry(newItem).State = EntityState.Modified;
        // await db.SaveChangesAsync();
        // return RedirectToAction("Index");
      }
      else {
        return View("Error");
      }
      // return View(cancellation);
    }

    // I

    //===================================================
    // Index() - GET: /Cancellations/
    //===================================================
    public ActionResult Index() {
      // var currentUser = manager.FindById(User.Identity.GetUserId());
      return View(db.CancellationFulls.ToListAsync());
    }

  }
}
