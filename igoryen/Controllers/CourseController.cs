﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using igoryen.Models;
using igoryen.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace igoryen.Controllers {
    [Authorize]
    public class CourseController : Controller {
        //private DataContext db = new DataContext();

        //==================================================
        // Bring in namespaces
        // 40. datacontext
        // 50. manager
        //==================================================
        private Repo_Course rc = new Repo_Course();
        private Repo_Student rs = new Repo_Student();
        private Repo_Faculty rf = new Repo_Faculty();

        private DataContext dc; // 40
        private UserManager<ApplicationUser> manager; // 50
        //private UserManager<IdentityUser> manager; // 50


        // Action methods alphabetically

        // C

        //======================================
        // Constructor
        //======================================
        public CourseController() {
            dc = new DataContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dc));
        }

        [Authorize(Roles = "Admin")] // 10
        public ActionResult All() {
            return View(dc.Courses.ToList());
        }
        //======================================
        // CourseCreate() - GET: /CourseCreate/Create
        //======================================
        public ActionResult CourseCreate() {
            ViewModels.CourseCreate newItem = new ViewModels.CourseCreate();

            ViewBag.students = rs.getStudentSelectList();
            ViewBag.faculties = rf.getFacultySelectList();

            return View("CourseCreate", newItem);
        }

        //======================================
        // CourseCreate() - POST: /CourseCreate/Create
        //======================================
        [HttpPost]
        public ActionResult CourseCreate(FormCollection form, ViewModels.CourseCreate newItem) {

            if (ModelState.IsValid) {
                try {
                    if (form.Count == 7) {
                        var addedItem = rc.createCourseAM(newItem, form[6]);
                        if (addedItem == null) {
                            return View("Error");
                        }
                        else {
                            return RedirectToAction("Details", new { Id = addedItem.CourseId });
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


        //==================================================
        // Create() - GET: /Course/Create
        //==================================================
        [Authorize(Roles = "Admin")]
        public ActionResult Create() {
            ViewBag.students = rs.getStudentSelectList();
            ViewBag.faculties = rf.getFacultySelectList();
            return View();
        }

        //======================================
        // Create() - POST: /Course/Create
        //======================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form) {
            //try {
            if (form.Count == 7) {
                rc.createCourse(form[1], form[2], form[3], form[4], form[5], form[6]);
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
        // Delete() - GET: /Course/Delete/5
        // 10. if id == null, don't delete
        //==================================================
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id) {
            if (id == null) { // 10
                ViewBag.ExceptionMessage = "That was an invalid record";
                return View("Error");
            }
            var course = rc.getCourseFullAM(id);
            if (course == null) {
                ViewBag.ExceptionMessage = "That record could not be deleted because it doesn't exist";
                return View("Error");
            }
            return View(course);
        }

        //==================================================
        // Delete() - POST: /Course/Delete/5
        //==================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            // Course course = db.Courses.Find(id);
            // db.Courses.Remove(course);
            // db.SaveChanges();
            rc.DeleteCourse(id);
            return RedirectToAction("Index");
        }

        //==================================================
        // Details() - GET: /Course/Details/5
        //==================================================
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id) {
            if (id == null) {
                var errors = new ViewModels.VM_Error();
                errors.ErrorMessages["ExceptionMessage"] = "No id specified";
                return View("Error", errors); // 12
            }
            return View(rc.getCourseFull(id));
        }

        //==================================================
        // Dispose()
        //==================================================
        /*
        protected override void Dispose(bool disposing) {
          if (disposing) {
            db.Dispose();
          }
          base.Dispose(disposing);
        }*/

        // E

        //==================================================
        // Edit() - // GET: /Course/Edit/5
        // 10. if id == null, do not query
        //==================================================
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id) {

            if (id == null) { // 10
                ViewBag.ExceptionMessage = "That was an invalid record";
                return View("Error");
            }

            var course = rc.getCourseFull(id);

            if (course == null) {
                ViewBag.ExceptionMessage = "That record could not be edited because it doesn't exist";
                return View("Error");
            }

            return View(course);
        }

        //==================================================
        // Edit() - //POST: /Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 10. If no ActionName("Edit"), it defaults to ActionName("Create")
        //==================================================
        [HttpPost, ActionName("Edit")] // 10
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseName,CourseCode,RoomNumber,RunTime,Faculty,Students")] CourseFull editItem) {
            if (ModelState.IsValid) {
                var newItem = rc.editCourseAM(editItem);
                if (newItem == null) {
                    ViewBag.ExceptionMessage = "Record " + editItem.CourseId + " was not found.";
                    return View("Error");
                }
                else {
                    return RedirectToAction("Index");
                }
                // db.Entry(editItem).State = EntityState.Modified;
                // db.SaveChanges();
                // return RedirectToAction("Index");
            } // if (ModelState.IsValid)
            else {
                return View("Error");
            }
            // return View(editItem);
        }

        // Index_Course_05 - doesn't work
        // Index_Course_06
        // GET: /Course/
        public ActionResult Index() {

            var currentUser = manager.FindById(User.Identity.GetUserId());
            
            if (currentUser == null) {
                ViewBag.ExceptionMessage0 = "CourseController.cs/Index()/currentUser: >>" + currentUser + "<<";
                return View("Error");
            }
            //===========================================================
            if (User.IsInRole("Student")) {
                List<Course> crss = new List<Course>(); // 20

                int dcc = dc.Courses.Count(); // 54
                for (int c = 0; c < dcc; c++) { // 30

                    int csc = dc.Courses.ElementAt(c).Students.Count(); // 55
                    for (int s = 0; s < csc; s++) { // 40

                        int scs = dc.Courses.ElementAt(c).Students.ElementAt(s).Courses.Count();
                        for (int cs = 0; cs < scs; cs++) { // 50
                            if (dc.Courses.ElementAt(c).Students.ElementAt(s).Courses.ElementAt(cs).User.Id == currentUser.Id) {
                                crss.Add(dc.Courses.ElementAt(c).Students.ElementAt(s).Courses.ElementAt(cs));
                            }

                        }

                    }
                } // 
                return View(crss.ToList());
            } // if role = student
            else {
                return View(dc.Courses.ToList().Where(course => course.User.Id == currentUser.Id));
            }
            //===========================================================
            //List<Course> courses = new List<Course>();
            //var course =  dc.Courses.FirstOrDefault(c => c.Users.FirstOrDefault(cr => cr.UserName))
            //return View(courses);

            //return View(dc.Courses.ToList().
            //    Where(course =>
            //    course.
            //    Users.
            //    Find(currentUser.Id) == currentUser.Id));


            //return View(dc.Courses.ToList().Where(
            //    course =>
            //    (course.
            //    Users.
            //    FirstOrDefault(
            //    user =>
            //        user.
            //        Id ==
            //        currentUser.
            //        Id)).ToString()
            //    == currentUser.Id));       

            //return View(
            //    dc.Courses.ToList().Where(
            //       course => (
            //            dc.Courses.Find(                                                                                    
            //                course.Users.Find(
            //                  user => user.Id == currentUser.Id
            //                )
            //            )

            //        ).ToString()
            //      == currentUser.Id
            //    )
            //);       

        }


    }
}
