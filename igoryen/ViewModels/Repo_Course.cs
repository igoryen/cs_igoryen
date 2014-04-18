using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using igoryen.Models;


namespace igoryen.ViewModels {
    public class Repo_Course : RepositoryBase {

        // methods alphabetically

        // C

        //======================================
        // createCourse()
        //======================================
        public CourseFull createCourse(string code, string name, string time, string room, string studPersIds, string facSenId) {

            Models.Course course = new Models.Course();
            course.CourseCode = code;
            course.CourseName = name;
            course.RunTime = time;
            course.RoomNumber = room;

            foreach (var item in studPersIds.Split(',')) {
                var intItem = Convert.ToInt32(item);
                var student = dc.Students.FirstOrDefault(students => students.PersonId == intItem);
                course.Students.Add(student);
            }
            //int fId = Convert.ToInt32(facId);
            course.Faculty = dc.Faculties.FirstOrDefault(fac => fac.SenecaId == facSenId);

            dc.Courses.Add(course);
            dc.SaveChanges();

            //return getCourseFull(course.CourseId);
            return getCourseFullAM(course.CourseId);
        }


        //======================================
        // CreateCourseAM() - with Automapper
        // 50. nulls are like time bombs
        //======================================
        public CourseFull createCourseAM(ViewModels.CourseCreate newItem, string facultyId) {

            Models.Course course = Mapper.Map<Models.Course>(newItem);
            int facId = Convert.ToInt32(facultyId);
            course.Faculty = dc.Faculties.FirstOrDefault(n => n.PersonId == facId);

            if (course.Faculty == null) return null; // 50

            dc.Courses.Add(course);
            dc.SaveChanges();

            return Mapper.Map<CourseFull>(course);
        }

        // D 

        //======================================
        // DeleteCourse()
        // 20. return [void] since the function's retval is void
        //======================================
        public void DeleteCourse(int? id) {
            var itemToDelete = dc.Courses.Find(id);
            if (itemToDelete == null) {
                return; // 20
            } // if
            else {
                try {
                    dc.Courses.Remove(itemToDelete);
                    dc.SaveChanges();
                }
                catch (Exception ex) {
                    throw ex;
                }
            } // else
        } // DeleteCourse()

        // E

        //======================================
        // EditCourse() - with Automapper
        //======================================
        public CourseFull editCourseAM(CourseFull editItem) {
            var itemToEdit = dc.Courses.Find(editItem.CourseId);
            if (itemToEdit == null) {
                return null;
            }
            else {
                dc.Entry(itemToEdit).CurrentValues.SetValues(editItem);
                dc.SaveChanges();
            }
            return Mapper.Map<CourseFull>(editItem);
        }


        // G

        //======================================
        // getCourseFull()
        // 30. cast from IEnumerable<> to List<>
        //====================================== 
        public CourseFull getCourseFull(int? CourseId) {
            var course = dc.Courses.Include("Students").Include("Faculty").SingleOrDefault(n => n.CourseId == CourseId);
            if (course == null) return null;

            CourseFull cf = new CourseFull();
            cf.CourseId = course.CourseId;
            cf.CourseCode = course.CourseCode;
            cf.CourseName = course.CourseName;
            cf.RunTime = course.RunTime;
            cf.RoomNo = course.RoomNumber;

            cf.Students = rs.toListOfStudentBase(course.Students);
            cf.FacultyFull = rf.getFacultyFull(course.Faculty.PersonId);

            return cf;
        }


        //======================================
        // getCourseFullAM() - with Automapper
        //====================================== 
        public CourseFull getCourseFullAM(int? id) {
            var course = dc.Courses.Include("Students").Include("Faculty").SingleOrDefault(n => n.CourseId == id);
            if (course == null) return null;
            else return Mapper.Map<CourseFull>(course);
        }

        public SelectList getSelectListOfCourse(string currentUserId) {

            IEnumerable<CourseBase> cbs = getListOfCourseBaseAM(currentUserId); // 15
            SelectList sl = new SelectList(cbs, "CourseId", "CourseCode"); // 16
            return sl;
        }

        public SelectList getCourseSelectList(string currentUserId) { // 47
            var lcb = new List<CourseBase>();
            lcb.Add(new CourseBase {
                CourseCode = "Select a course code",
                CourseId = -1
            });
            foreach (var item in getListOfCourseBaseAM(currentUserId)) { // 48
                lcb.Add(item); // 49
            }
            SelectList sl = new SelectList(lcb.ToList(), "CourseId", "CourseCode"); // 16
            return sl; // 50
        }

        //public IEnumerable<CourseBase> getListOfCourseBase(string currentUserId) {
        //    var courses = dc.Courses.
        //                        Where(course =>
        //                        course.
        //                        User.
        //                        Id ==
        //                        currentUserId).ToList(); // 38
        //    var courses = dc.Courses.OrderBy(c => c.CourseCode);
        //    if (courses == null) return null;
        //    List<CourseBase> lcb = new List<CourseBase>();
        //    foreach (var item in courses) {
        //        CourseBase cb = new CourseBase();
        //        cb.CourseId = item.CourseId;
        //        cb.CourseCode = cb.CourseCode;
        //        lcb.Add(cb);
        //    }
        //    return lcb.ToList();
        //}

        //======================================
        // getListOfCourseBaseAM() - with automapper
        //====================================== 
        public IEnumerable<CourseBase> getListOfCourseBaseAM(string currentUserId) {
            var courses = dc.Courses.
                Where(course =>
                    course.
                    User.
                    Id == 
                    currentUserId).ToList(); // 38
            if (courses == null) return null;
            return Mapper.Map<IEnumerable<CourseBase>>(courses);
        }

        //public IEnumerable<Course> getListOfCourseAMbyCurrentUser(string currentUserId) {
        //    //int currentUserIdInt = Convert.ToInt32(currentUserId);

        //    var courses = dc.Courses.ToList().Where(
        //        course => 
        //            course.User.Id 
        //            == currentUserId);
        //        //course != null && 
                
            
        //    //if (courses == null) return null;
        //    if (courses.Count() < 1) return null;

        //    return Mapper.Map<IEnumerable<Course>>(courses);
        //}

        // write another code that takes an id

        //======================================
        // getListOfCourseBase() 
        //====================================== 

        public static List<CourseBase> getListOfCourseBase(List<igoryen.Models.Course> ls) {
            List<CourseBase> nls = new List<CourseBase>();

            foreach (var item in ls) {
                CourseBase co = new CourseBase();
                co.CourseCode = item.CourseCode;
                co.CourseId = item.CourseId;
                nls.Add(co);
            }

            return nls;
        }



        // I

        //======================================
        // Implementation details
        //======================================
        Repo_Faculty rf;
        Repo_Student rs;


        // R

        //======================================
        // Repo_Course() - Constructor
        //======================================
        public Repo_Course() {
            //rf = new Repo_Faculty();
            //rs = new Repo_Student();
        }

        // T

        //======================================
        // toListOfCourseBase()
        //====================================== 
        public List<CourseBase> toListOfCourseBase(List<Models.Course> courses) {
            List<CourseBase> lcb = new List<CourseBase>();
            foreach (var item in courses) {
                CourseBase cb = new CourseBase();
                cb.CourseId = item.CourseId;
                cb.CourseCode = item.CourseCode;
                lcb.Add(cb);
            }
            return lcb;
        }




    }
}