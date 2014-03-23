using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using igoryen.ViewModels;
using igoryen.Models;

namespace igoryen.Controllers {
  public class MessagesController : Controller {
    private DataContext db = new DataContext();

    //==================================================
    // Bring in namespaces
    //==================================================
    private Repo_Course rc = new Repo_Course();
    private Repo_Student rs = new Repo_Student();
    private Repo_Faculty rf = new Repo_Faculty();
    private Repo_Message rm = new Repo_Message();


    // Methods alphabetically

    // C

    // C

    //======================================
    // CourseCreate() - GET: /Course/Create
    //======================================
    public ActionResult MessageCreate() {
      ViewModels.MessageCreate newItem = new ViewModels.MessageCreate();

      //ViewBag.students = rs.getStudentSelectList();
      ViewBag.faculties = rf.getFacultySelectList();

      return View("MessageCreate", newItem);
    }

    //======================================
    // CourseCreate() - POST: /Course/Create
    // 20. the number of forms is 
    //======================================
    [HttpPost]
    public ActionResult MessageCreate(FormCollection form, ViewModels.MessageCreate newItem) {
      if (ModelState.IsValid) {
        try {
          if (form.Count == 4) { // 20
            var addedItem = rm.createMessageAM(newItem, form[4]);
            if (addedItem == null) {
              return View("Error");
            }
            else {
              return RedirectToAction("Details", new { Id = addedItem.MessageId });
            }
          } // if (form.Count == 4)
          return RedirectToAction("Index");
        } // try
        catch (Exception e) {
          ViewBag.ExceptionMessage = e.Message;
          return View("Error");
        } // catch
      } // if (ModelState.IsValid)
      else {
        return View("Error");
      }
    } // CourseCreate()

    //===================================================
    // Create() - GET: /Messages/Create
    //===================================================
    public ActionResult Create() {
      //ViewBag.students = rs.getStudentSelectList();
      ViewBag.faculties = rf.getFacultySelectList();
      return View();
    }

    //===================================================
    // Create() - POST: /Messages/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //===================================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "MessageId,CourseName,Body,CustomMsg,Date,Time")] MessageFull messagefull) {
      if (ModelState.IsValid) {
        db.MessageFulls.Add(messagefull);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      return View(messagefull);
    }

    //===================================================
    // Delete() - GET: /Messages/Delete/5
    //===================================================
    public async Task<ActionResult> Delete(int? id) {
      if (id == null) {
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ViewBag.ExceptionMessage = "That was an invalid record";
        return View("Error");
      }
      //MessageFull messagefull = await db.MessageFulls.FindAsync(id);
      var messagefull = rm.getMessageFullAM(id);
      if (messagefull == null) {
        //return HttpNotFound();
        ViewBag.ExceptionMessage = "That record could not be deleted because it doesn't exist";
        return View("Error");
      }
      return View(messagefull);
    }

    //===================================================
    // DeleteConfirmed() - POST: /Messages/Delete/5
    //===================================================
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id) {
      //MessageFull messagefull = await db.MessageFulls.FindAsync(id);
      //db.MessageFulls.Remove(messagefull);
      //await db.SaveChangesAsync();
      rm.DeleteMessage(id);
      return RedirectToAction("Index");
    }

    //===================================================
    // Details() - GET: /Messages/Details/5
    //===================================================
    public async Task<ActionResult> Details(int? id) {
      /*if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      MessageFull messagefull = await db.MessageFulls.FindAsync(id);
      if (messagefull == null) {
        return HttpNotFound();
      }
      return View(messagefull);
      */
      return View(rm.getMessageFullAM(id));
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

    //===================================================
    // Edit() - GET: /Messages/Edit/5
    //===================================================
    public async Task<ActionResult> Edit(int? id) {
      if (id == null) {
        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ViewBag.ExceptionMessage = "That was an invalid record";
        return View("Error");
      }
      // MessageFull messagefull = await db.MessageFulls.FindAsync(id);
      var messagefull = rm.getMessageFullAM(id);
      if (messagefull == null) {
        // return HttpNotFound();
        ViewBag.ExceptionMessage = "That record could not be edited because it doesn't exist";
        return View("Error");
      }
      return View(messagefull);
    }

    //===================================================
    // Edit() - POST: /Messages/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //===================================================
    [HttpPost, ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "MessageId,CourseName,Body,CustomMsg,Date,Time")] MessageFull editItem) {
      if (ModelState.IsValid) {
        var newItem = rm.editMessageAM(editItem);
        if (newItem == null) {
          ViewBag.ExceptionMessage = "Record " + editItem.MessageId + " was not found.";
          return View("Error");
        }
        else {
          return RedirectToAction("Index");
        }
        // db.Entry(messagefull).State = EntityState.Modified;
        // await db.SaveChangesAsync();
        // return RedirectToAction("Index");
      }
      else {
        return View("Error");
      }
      // return View(messagefull);
    }
    
    //===================================================
    // Index() - GET: /Messages/
    //===================================================
    public async Task<ActionResult> Index() {
      return View(await db.MessageFulls.ToListAsync());
    }

  }
}
