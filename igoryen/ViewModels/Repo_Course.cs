using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace igoryen.ViewModels {
  public class Repo_Course : RepositoryBase {

    // methods alphabetically

    // C

    //======================================
    // CreateCourse() - with Automapper
    // 50. nulls are like time bombs
    //======================================
    public CourseFull amCreateCourse(ViewModels.CourseCreate newItem, string d) {
      Models.Course course = Mapper.Map<Models.Course>(newItem);
      int did = Convert.ToInt32(d);
      course.Faculty = dc.Faculties.FirstOrDefault(n => n.Id == did);

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
    public CourseFull amEditCourse(CourseFull editItem) {
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
    // getCourseFull() - with Automapper
    //====================================== 
    public CourseFull amGetCourseFull(int? id) {
      var course = dc.Courses.Include("Students").Include("Faculty").SingleOrDefault(n => n.Id == id);
      if (course == null) return null;
      else return Mapper.Map<CourseFull>(course);
    }

    //======================================
    // getListOfCourseBase() - with automapper
    //====================================== 
    public IEnumerable<CourseBase> amGetListOfCourseBase() {
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
        co.CourseId = item.Id;
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
      rf = new Repo_Faculty();
      rs = new Repo_Student();
    }

    // T

    //======================================
    // toListOfCourseBase()
    //====================================== 
    public List<CourseBase> toListOfCourseBase(List<Models.Course> courses) {
      List<CourseBase> lcb = new List<CourseBase>();
      foreach (var item in courses) {
        CourseBase cb = new CourseBase();
        cb.CourseId = item.Id;
        cb.CourseCode = item.CourseCode;
        lcb.Add(cb);
      }
      return lcb;
    }




  }
}