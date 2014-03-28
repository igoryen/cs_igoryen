using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;


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
    public CourseFull getCourseFull(int? id) {
      var course = dc.Courses.Include("Students").Include("Faculty").SingleOrDefault(n => n.CourseId == id);
      if (course == null) return null;

      CourseFull courseFull = new CourseFull();
      courseFull.CourseId = course.CourseId;
      courseFull.CourseCode = course.CourseCode;
      courseFull.CourseName = course.CourseName;
      courseFull.RunTime = course.RunTime;
      courseFull.RoomNo = course.RoomNumber;
      courseFull.Students = rs.toListOfStudentBase(course.Students);
      courseFull.Faculty = rf.getFacultyFull(course.Faculty.PersonId);

      return courseFull;
    }


    //======================================
    // getCourseFullAM() - with Automapper
    //====================================== 
    public CourseFull getCourseFullAM(int? id) {
      var course = dc.Courses.Include("Students").Include("Faculty").SingleOrDefault(n => n.CourseId == id);
      if (course == null) return null;
      else return Mapper.Map<CourseFull>(course);
    }

    //======================================
    // getCourseSelectList()
    //======================================
    public SelectList getCourseSelectList() {
      SelectList sl = new SelectList(getListOfCourseBaseAM(), "CourseId", "CourseCode");
      return sl;
    }
    //======================================
    // getListOfCourseBaseAM() - with automapper
    //====================================== 
    public IEnumerable<CourseBase> getListOfCourseBaseAM() {
      var courses = dc.Courses.OrderBy(c => c.CourseCode);
      if (courses == null) return null;
      return Mapper.Map<IEnumerable<CourseBase>>(courses);
    }

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