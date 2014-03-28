using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using igoryen.Models;
using igoryen.ViewModels;
using AutoMapper;

namespace igoryen.ViewModels {
  public class Repo_Faculty : RepositoryBase {
    public List<Faculty> Faculties { get; set; }

    // methods alphabetically

    // C

    //==============================================================
    // createFaculty() - create a Faculty record
    //==============================================================
    /*
    public FacultyFull createFaculty(FacultyFull fa) {
      Faculty fac = new Faculty(fa.FirstName, fa.LastName, fa.Phone, fa.FacultyId);
      fac.Id = Faculties.Max(n => n.Id) + 1;
      Faculties.Add(fac);
      return fa;
    }
    */

    //==================================================
    // CreateFaculty() - using Automapper
    //==================================================
    public FacultyFull createFacultyAM(ViewModels.FacultyCreate newItem, string d) {
      Models.Faculty faculty = Mapper.Map<Models.Faculty>(newItem);
      //int did = Convert.ToInt32(d);
      //faculty.School = dc.Faculties.FirstOrDefault(n => n.School == d);

      dc.Faculties.Add(faculty);
      dc.SaveChanges();

      return Mapper.Map<FacultyFull>(faculty);
    }


    // D 

    //==================================================
    // DeleteFaculty()
    //==================================================
    public void deleteFaculty(int? id) {
      var itemToDelete = dc.Faculties.Find(id);
      if (itemToDelete == null) {
        return;
      }
      else {
        try {
          dc.Faculties.Remove(itemToDelete);
          dc.SaveChanges();
        }
        catch (Exception ex) {
          throw ex;
        }
      }
    }

    // E

    //==================================================
    // EditFaculty() - with Automapper
    //==================================================
    public FacultyFull editFacultyAM(FacultyFull editItem) {
      var itemToEdit = dc.Faculties.Find(editItem.FacultyId);
      if (itemToEdit == null) {
        return null;
      }
      else {
        dc.Entry(itemToEdit).CurrentValues.SetValues(editItem);
        dc.SaveChanges();
      }
      return Mapper.Map<FacultyFull>(editItem);
    }


    // G

    //==============================================================
    // getFacultyFull()
    //==============================================================
    
    public FacultyFull getFacultyFull(int? id) {
      //var st = dc.Students.FirstOrDefault(n => n.Id == id);
      var f = dc.Faculties.Include("Courses").FirstOrDefault(n => n.PersonId == id);

      if (f == null) return null;

      FacultyFull ff = new FacultyFull();

      ff.SenecaId = f.SenecaId;
      ff.FirstName = f.FirstName;
      ff.LastName = f.LastName;
      ff.Phone = f.Phone;
      ff.Courses = Repo_Course.getListOfCourseBase(f.Courses);
      return ff;

    }
     

    //==================================================
    // getFacultyFullAM() - with Automapper
    //================================================== 
    public FacultyFull getFacultyFullAM(int? id) {
      var faculty = new Faculty();
      faculty = dc.Faculties.Include("Courses").SingleOrDefault(n => n.PersonId == id);
      if (faculty == null) return null;
      else return Mapper.Map<FacultyFull>(faculty);
    }



    //==============================================================
    // getFacultyNames() - as an IEnumerable
    //==============================================================
    public IEnumerable<FacultyName> getListOfFacultyName() { // 95

      var ls = this.Faculties.OrderBy(n => n.LastName);    // 100
      List<FacultyName> rls = new List<FacultyName>();    // 105

      foreach (var item in ls) {      // 110
        FacultyName row = new FacultyName(); // 115

        row.FacultyId = item.PersonId; // 50
        row.FirstName = item.FirstName;
        row.LastName = item.LastName;

        rls.Add(row); // 51 
      }
      return rls; // 52
    }


    //==============================================================
    // getFacultyPublic() - return single faculty row by id
    //==============================================================
    public FacultyPublic getFacultyPublic(int? id) {
      var faculty = Faculties.FirstOrDefault(n => n.PersonId == id);
      FacultyPublic fp = new FacultyPublic();
      fp.FirstName = faculty.FirstName;
      fp.LastName = faculty.LastName;
      //fp.Phone = faculty.Phone;
      //fp.FacultyNumber = faculty.FacultyNumber;
      return fp;
    }


    //==============================================================
    // getFacultySelectList()
    //==============================================================
    public SelectList getFacultySelectList() {
      SelectList sl = new SelectList(getListOfFacultyBaseAM(), "SenecaId", "FirstName", "LastName");
      return sl;
    }


    //==================================================
    // getListOfFacultyBaseAM() - with automapper
    //================================================== 
    public IEnumerable<FacultyBase> getListOfFacultyBaseAM() {
      var faculties = dc.Faculties.OrderBy(f => f.SenecaId);
      if (faculties == null) return null;
      return Mapper.Map<IEnumerable<FacultyBase>>(faculties);
    }

    //==================================================
    // getListOfFacultyBase() - as a List
    //================================================== 

    public static List<FacultyBase> getListOfFacultyBase(List<igoryen.Models.Faculty> ls) {
      List<FacultyBase> lfb = new List<FacultyBase>();

      foreach (var item in ls) {
        FacultyBase fb = new FacultyBase();
        fb.FacultyId = item.PersonId;
        fb.SenecaId = item.SenecaId;
        fb.FirstName = item.FirstName;
        fb.LastName = item.LastName;
        lfb.Add(fb);
      }

      return lfb;
    }


    //==============================================================
    // getListOfFacultyBase() - as an IEnumerable
    //==============================================================
    public IEnumerable<FacultyBase> getListOfFacultyBase() {
      var faculties = dc.Faculties.OrderBy(f => f.PersonId); // ???
      if (faculties == null) return null;

      List<FacultyBase> lfb = new List<FacultyBase>();

      foreach (var item in faculties) {
        FacultyBase fb = new FacultyBase();
        fb.FacultyId = item.PersonId;
        fb.SenecaId = item.SenecaId;
        lfb.Add(fb);
      }
      return lfb;
    }

    //==================================================
    // getListOfFacultyName() - with automapper
    //================================================== 
    public IEnumerable<FacultyName> getListOfFacultyNameAM() {
      var faculties = dc.Faculties.OrderBy(f => f.PersonId);
      if (faculties == null) return null;
      return Mapper.Map<IEnumerable<FacultyName>>(faculties);
    }


    //==============================================================
    // getListOfFacultyPublic() - return a list / collection of faculties
    //==============================================================
    public IEnumerable<FacultyPublic> getListOfFacultyPublic() { // 1
      var faculties = Faculties.OrderBy(n => n.SenecaId); // 20
      List<FacultyPublic> lfp = new List<FacultyPublic>(); // 25

      foreach (var item in faculties) {  // 30
        FacultyPublic fp = new FacultyPublic();   // 35

        fp.SenecaId = item.SenecaId;  // 40 
        fp.FirstName = item.FirstName;
        fp.LastName = item.LastName;
        fp.Phone = item.Phone;

        lfp.Add(fp);  // 45 
      }
      return lfp;  // 50
    }


    // R

    /*
     public Repo_Faculty() {
      this.Faculties = (List<Faculty>)HttpContext.Current.Application["Faculties"];
    }

     */
    //==================================================
    // Repo_Faculty() - Constructor
    //==================================================
    public Repo_Faculty() {
      //rc = new Repo_Course();
      //rs = new Repo_Student();
    }

    // S
    //==============================================================
    // sortFaculties()
    //==============================================================
    public IEnumerable<Faculty> sortFaculties() {
      var retval = Faculties.OrderBy(n => n.PersonId);
      // if (retval == null) { System.Console.WriteLine()}
      return retval;
    }

    // T

    //==================================================
    // toFacultyFull()
    //==================================================
    public FacultyFull toFacultyFull(Models.Faculty faculty) {
      if (faculty == null) return null;

      FacultyFull ff = new FacultyFull();
      ff.FacultyId = faculty.PersonId;
      ff.SenecaId = faculty.SenecaId;
      ff.FirstName = faculty.FirstName;
      ff.LastName = faculty.LastName;
      ff.Phone = faculty.Phone;
      ff.Courses = new List<CourseBase>();
      foreach (var item in faculty.Courses) {
        CourseBase cb = new CourseBase();
        cb.CourseId = item.CourseId;
        cb.CourseCode = item.CourseCode;
        ff.Courses.Add(cb);
      }
      return ff;
    }

    //==================================================
    // toListOfFacultyBase()
    //================================================== 
    public List<FacultyBase> toListOfFacultyBase(List<Models.Faculty> faculties) {
      List<FacultyBase> lfb = new List<FacultyBase>();
      foreach (var item in faculties) {
        FacultyBase fb = new FacultyBase();
        fb.FacultyId = item.PersonId;
        fb.SenecaId = item.SenecaId;
        lfb.Add(fb);
      }
      return lfb;
    }



    Repo_Course rc;
    Repo_Student rs;


  }
}