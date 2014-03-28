using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using igoryen.Models;


namespace igoryen.Models {
  public class Manager {

    DataContext dc = new DataContext();

    // Methods alphabetically

    // C

    //===================================================
    // createStudent()
    //===================================================
    public Student createStudent(Student stu) {

      stu.PersonId = Students.Max(n => n.PersonId) + 1;
      //Students.Add(stu);
      dc.Students.Add(stu);
      dc.SaveChanges();
      return stu;
    }
    //===================================================
    // createStudent()
    //===================================================
    public Student createStudent(string fName, string lName, string pNumber, string sid) {
      var st = new Student(fName, lName, pNumber, sid);

      st.PersonId = Students.Last().PersonId + 1;
      //Students.Add(st);
      dc.Students.Add(st);
      dc.SaveChanges();
      return st;
    }


    // E

    //===================================================
    // editStudent()
    //===================================================
    public Student editStudent(int id, string fName, string lName, string pNumber, string senId) {
      var stu = Students.FirstOrDefault(b => b.PersonId == id);

      stu.FirstName = fName;
      stu.LastName = lName;
      stu.Phone = pNumber;
      stu.SenecaId = senId;
      dc.SaveChanges();
      return stu;
    }

    // G

    //===================================================
    // getSelectList()
    //===================================================
    //this is essentailly a ViewModel because the SelectList only carries the data needed to display a list control
    //don't do this until week two.
    public SelectList getSelectList() {
      //SelectList sl = new SelectList(dc.Courses, "Id", "CourseCode");
      SelectList sl = new SelectList(Students, "Id", "StudentNumber");
      return sl;
    }

    //===================================================
    // getStudent() 
    //===================================================
    public Student getStudent(int? id) {
      return Students.FirstOrDefault(n => n.PersonId == id);
    }


    // M

    //===================================================
    // Manager() - constructor
    //===================================================
    public Manager() {
      this.Students = (List<Student>)HttpContext.Current.Application["Students"];
    }

    // S

    //===================================================
    // sortStudents()
    //===================================================
    public IEnumerable<Student> sortStudents() {
      //return this.Students.OrderBy(n => n.Id);
      return dc.Students.OrderBy(n => n.PersonId);
    }

    public List<Student> Students { get; set; }
  }
}