using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using igoryen.Models;
using System.Data.Entity.Validation;

namespace igoryen.ViewModels {
    public class Repo_Cancellation : RepositoryBase {

        // methods alphabetically

        // C

        //======================================
        // CreateCancellation() - with Automapper
        // 50. nulls are like time bombs
        //======================================
        public CancellationFull createCancellation(string coursesIds, string date, string msg/*, string currentUserId*/) {

            Models.Cancellation cancellation = new Models.Cancellation();

            foreach (var item in coursesIds.Split(',')) {
                var intItem = Convert.ToInt32(item);
                var course = dc.Courses.FirstOrDefault(crs => crs.CourseId == intItem);
                //cancellation.Course.Add(course);
            }

            cancellation.Date = date;
            cancellation.Message = msg;
            //cancellation.Faculty = 

            dc.Cancellations.Add(cancellation);
            dc.SaveChanges();

            //return Mapper.Map<CancellationFull>(cancellation);
            return getCancellationFullAM(cancellation.CancellationId);
        }

        //======================================
        // CreateCancellationAM() - with Automapper
        // 50. nulls are like time bombs
        //======================================
        public CancellationFull createCancellationAM(ViewModels.CancellationCreate newItem, string courseId) {

            Models.Cancellation cancellation = Mapper.Map<Models.Cancellation>(newItem);
            int courseIdInt = Convert.ToInt32(courseId);

            //======= alternating ==============================
            // cancellation.Course = dc.Courses.FirstOrDefault(n => n.CourseId == courseIdInt);
            //--------------------------------------------------
            var course = dc.Courses.FirstOrDefault(n => n.CourseId == courseIdInt);
            cancellation.CourseBase.CourseCode = course.CourseCode;
            cancellation.CourseBase.CourseId = course.CourseId;
            cancellation.CourseBase.CourseName = course.CourseName;
            //==================================================

            if (cancellation.CourseBase == null) return null; // 50

            dc.Cancellations.Add(cancellation);
            dc.SaveChanges();

            return Mapper.Map<CancellationFull>(cancellation);
        }

        //=====================================
        // createCancellation(CancellationCreateForHttpPost)
        //=====================================
        public CancellationFull createCancellation(CancellationCreateForHttpPost newItem) { // 52
            var course = dc.Courses.Find(newItem.CourseId); // 40
            Models.Cancellation cancellation = new Models.Cancellation(); // 41

            cancellation.CancellationId = newItem.CancellationId;
            cancellation.Date = newItem.Date;
            cancellation.Message = newItem.Message;
            //======= alternating ============================
            // cancellation.Course = course;
            //------------------------------------------------
            cancellation.CourseBase = new CourseBase();
            cancellation.CourseBase.CourseCode = course.CourseCode;
            cancellation.CourseBase.CourseId = course.CourseId;
            cancellation.CourseBase.CourseName = course.CourseName;
            //================================================

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
            

            return getCancellationFull(cancellation.CancellationId);
        }

        // D 

        //======================================
        // DeleteCancellation()
        // 20. return [void] since the function's retval is void
        //======================================
        public void DeleteCancellation(int? id) {
            var itemToDelete = dc.Cancellations.Find(id);
            if (itemToDelete == null) {
                return; // 20
            } // if
            else {
                try {
                    dc.Cancellations.Remove(itemToDelete);
                    dc.SaveChanges();
                }
                catch (Exception ex) {
                    throw ex;
                }
            } // else
        } // DeleteCancellation()

        // E

        //======================================
        // EditCancellation() - with Automapper
        //======================================
        public CancellationFull editCancellationAM(CancellationFull editItem) {
            var itemToEdit = dc.Cancellations.Find(editItem.CancellationId);
            if (itemToEdit == null) {
                return null;
            }
            else {
                dc.Entry(itemToEdit).CurrentValues.SetValues(editItem);
                dc.SaveChanges();
            }
            return Mapper.Map<CancellationFull>(editItem);
        }


        // G

        //======================================
        // getCancellationFull
        //====================================== 
        public CancellationFull getCancellationFull(int? CancellationId) {
            if (CancellationId == null) return null;

            var cancellation = dc.Cancellations.Include("Course").SingleOrDefault(n => n.CancellationId == CancellationId); // 44
            if (cancellation == null) return null;

            CancellationFull ccf = new CancellationFull(); // 45
            ccf.CancellationId = cancellation.CancellationId;
            ccf.Date = cancellation.Date;
            ccf.Message = cancellation.Message;
            ccf.CourseBase = 
                rc.getCourseBase(
                cancellation.CourseBase.
                CourseId);

            return ccf;
        }

        //======================================
        // getCancellationFullAM - with Automapper
        //====================================== 
        public CancellationFull getCancellationFullAM(int? id) {
            if (id == null) return null;
            var cancellation = dc.Cancellations.Include("Faculty").SingleOrDefault(n => n.CancellationId == id);
            if (cancellation == null) return null;
            else return Mapper.Map<CancellationFull>(cancellation);
        }

        //======================================
        // getListOfCancellationBase() - with automapper
        //====================================== 
        public IEnumerable<CancellationBase> getListOfCancellationBaseAM() {
            var cancellations = dc.Cancellations.OrderBy(c => c.CancellationId);
            if (cancellations == null) return null;
            return Mapper.Map<IEnumerable<CancellationBase>>(cancellations);
        }

        //======================================
        // getListOfCancellationBase() 
        //====================================== 
        /*
        public static List<CancellationBase> getListOfCancellationBase(List<igoryen.Models.Cancellation> ls) {
          List<CancellationBase> lcb = new List<CancellationBase>();

          foreach (var item in ls) {
            CancellationBase cb = new CancellationBase();
            cb.CancellationId = item.CancellationId;
            cb.Date = item.Date;
            lcb.Add(cb);
          }

          return lcb;
        }*/

        // I

        //======================================
        // Implementation details
        //======================================
        Repo_Faculty rf;
        Repo_Course rc;


        // R

        //======================================
        // Repo_Course() - Constructor
        //======================================
        public Repo_Cancellation() {
            rf = new Repo_Faculty();
            rc = new Repo_Course();
        }

        // T

        //======================================
        // toListOfCancellationBase()
        //======================================
        /*
        public List<CancellationBase> toListOfCancellationBase(List<Models.Cancellation> cancellations) {
          List<CancellationBase> lcb = new List<CancellationBase>();
          foreach (var item in cancellations) {
            CancellationBase cb = new CancellationBase();
            cb.CancellationId = item.CancellationId;
            cb.Date = item.Date;
            lcb.Add(cb);
          }
          return lcb;
        }*/
    }
}