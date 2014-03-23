using igoryen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace igoryen.ViewModels {
  public class Repo_Student : RepositoryBase {

    public List<Student> Students { get; set; }

    // methods alphabetically

    // C

    //======================================
    // createStudent(StudentFull st)
    /* 140. make a new 4-column row "Student" and fill it out
     * 145. savechanges is the equivalent to a database commit statement
     * 150. return a copy of the new Student as a StudentFull
     */
    //======================================
    /*
    public StudentFull createStudent(StudentFull st) {

      Student stu = new Student(st.FirstName, st.LastName, st.Phone, st.StudentNumber); // 140

      dc.Students.Add(stu);
      dc.SaveChanges(); // 145

      return getStudentFull(stu.Id); // 150
    }*/

    //======================================
    // createStudent(StudentFull st, string ids)
    /* 100. make a new 4-column row "Student" and fill it out 
     * 105. make a list/column of int32 objects
     * 110. reformat ids into ("n,n,n,...") where n is an numeric character
            split the string into an array of individual characters
     * 115. convert each character to an int32 and store in ls
     * 120. iterate through ls and for each id in the list, add a Course to the student's Courses collection
     * 125. add the filled out 4-column row "Student" to the DataContext
     * 130. savechanges is the equivalent to a database "commit" statement
     * 135. return a copy of the new 4-column row "Student" as a StudentFull
     */

    //======================================
    public StudentFull createStudent(StudentFull st, string ids) {
      Student stu = new Student(st.FirstName, st.LastName, st.Phone, st.StudentNumber); // 100 

      /*
      stu.Id = Students.Max(n => n.Id) + 1;
      dc.Students.Add(stu);
      return st;
      */

      List<Int32> ls = new List<int>(); // 105
      var nums = ids.Split(','); // 110

      foreach (var item in nums) { // 115
        ls.Add(Convert.ToInt32(item));
      }

      foreach (var item in ls) { // 120
        stu.Courses.Add(dc.Courses.FirstOrDefault(n => n.Id == item));
      }

      dc.Students.Add(stu); // 125
      dc.SaveChanges(); // 130

      return getStudentFull(stu.Id); // 135

    }

    // G

    //======================================
    // getListOfStudentBase()
    //======================================
    public IEnumerable<StudentBase> getListOfStudentBase() {
      var students = dc.Students.OrderBy(n => n.Id);
      //var ls = this.Students.OrderBy(n => n.Id);

      if (students == null) return null;

      List<StudentBase> lsb = new List<StudentBase>();

      foreach (var item in students) {
        StudentBase sb = new StudentBase();
        sb.StudentId = item.Id;
        sb.StudentNumber = item.StudentNumber;
        lsb.Add(sb);
      }

      return lsb;
    }

    //======================================
    // getListOfStudentFull()
    //======================================
    public IEnumerable<StudentFull> getListOfStudentFull() { // 55

      //var ls = dc.Students.Include("Courses").OrderBy(n => n.LastName);     // 60
      var st = this.Students.OrderBy(n => n.LastName);     // 60
      List<StudentFull> rls = new List<StudentFull>();   // 65

      foreach (var item in st) {  // 70
        StudentFull row = new StudentFull();   // 75

        row.StudentId = item.Id;  // 80
        row.StudentNumber = item.StudentNumber;
        row.FirstName = item.FirstName;
        row.LastName = item.LastName;
        row.Phone = item.Phone;
        row.Courses = Repo_Course.getListOfCourseBase(item.Courses);

        rls.Add(row);  // 85
      }
      return rls; // 90
    }

    //======================================
    // getListOfStudentFullAM() - with Automapper
    //======================================
    public IEnumerable<StudentFull> getListOfStudentFullAM() {
      var students = dc.Students.OrderBy(n => n.LastName);
      if (students == null) return null;
      return Mapper.Map<IEnumerable<StudentFull>>(students);
    }


    //======================================
    // getStudentsFull() -- gets a List of all Students, mapped to a List of StudentFull objects, sorted by LastName
    /* 55. produce a 5-column table of students sorted by LastName
     * 60. make a list of students ordered by StudentNo
     * 65. make an empty 5-column table "StudentFull"
     * 70. loop through the list of students ordered by StudentNo
     * 75. make a 5-column row "StudentFull"
     * 80. fill out the row's 5 columns with data from students list
     * 85. add the 5-column row StudentPublic to the 5-column table "StudentFull"
     * 90. produce the filled out 5-column table "StudentFull" 
     */
    //======================================
    public StudentFull getStudentFull(int? id) {
      //var st = dc.Students.FirstOrDefault(n => n.Id == id);
      var st = Students.FirstOrDefault(n => n.Id == id);

      StudentFull stu = new StudentFull();
      stu.FirstName = st.FirstName;
      stu.LastName = st.LastName;
      stu.Phone = st.Phone;
      stu.StudentNumber = st.StudentNumber;
      stu.Courses = Repo_Course.getListOfCourseBase(st.Courses);

      return stu;
    }


    //======================================
    // getStudentNames() -- gets a List of all Students, mapped to a List of StudentName objects, sorted by LastName 
    /* 45. take a table... ??? how to use it???
     * 46. make a list of students ordered by LastName. ls = list
     * 47. make a 3-column table "StudentName". rls = ready list
     * 48. loop through the list of students ordered by LastName
     * 49. make a 3-column table row "StudentName"
     * 50. fill out the 3-column table row
     * 51. add the StudentPublic to the list for StudentPublic's
     * 52. 
     */
    //======================================
    public IEnumerable<StudentName> getStudentNames() { // 95

      //var st = this.Students.OrderBy(n => n.LastName);    // 100
      var st = dc.Students.OrderBy(n => n.LastName);    // 100

      List<StudentName> lsn = new List<StudentName>();    // 105

      foreach (var item in st) {      // 110
        StudentName row = new StudentName(); // 115

        row.StudentId = item.Id; // 50
        row.FirstName = item.FirstName;
        row.LastName = item.LastName;

        lsn.Add(row); // 51 
      }
      return lsn; // 52
    }

    //======================================
    // getStudentNamesAM() -- with Automapper
    //======================================
    public IEnumerable<StudentName> getStudentNamesAM() { // 95

      //var st = this.Students.OrderBy(n => n.LastName);    // 100
      var sn = dc.Students.OrderBy(n => n.LastName);    // 100

      if (sn == null) return null;

      return Mapper.Map<IEnumerable<StudentName>>(sn);

    }

    //======================================
    // getStudentsPublic()  -- gets a List of all Students, mapped to a List of StudentPublic objects, sorted by StudentNumber
    /* 20. make a list of students ordered by StudentNo
     * 25. make an empty 4-column table "StudentPublic"
     * 30. loop through the list of students ordered by StudentNo
     * 35. make a 4-column row (StudentPublic)
     * 40. fill out the row's 4 columns with data from students list
     * 45. add the 4-column row StudentPublic to the 4-column table for StudentPublic's
     * 50. produce the filled out 4-column table of StudentPublic's 
     */
    //======================================
    public IEnumerable<StudentPublic> getStudentsPublic() { // 1
      var st = dc.Students.OrderBy(n => n.StudentNumber); // 20
      //var st = this.Students.OrderBy(n => n.StudentNumber); // 20
      List<StudentPublic> rls = new List<StudentPublic>(); // 25

      foreach (var item in st) {  // 30
        StudentPublic row = new StudentPublic();   // 35

        row.StudentNumber = item.StudentNumber;  // 40 
        row.FirstName = item.FirstName;
        row.LastName = item.LastName;
        row.Phone = item.Phone;

        rls.Add(row);  // 45 
      }
      return rls;  // 50
    }

    //======================================
    // getStudentPublic(int? id)
    //======================================
    public StudentPublic getStudentPublic(int? id) {
      //var st = dc.Students.FirstOrDefault(n => n.Id == id);
      var st = Students.FirstOrDefault(n => n.Id == id);

      StudentPublic stu = new StudentPublic();
      stu.FirstName = st.FirstName;
      stu.LastName = st.LastName;
      stu.Phone = st.Phone;
      stu.StudentNumber = st.StudentNumber;

      return stu;

    }




    //======================================
    // getStudentSelectList()
    //======================================

    public SelectList getStudentSelectList() {
      SelectList sl = new SelectList(getListOfStudentBase(), "StudentId", "StudentNumber");
      return sl;
    }




    // R

    //======================================
    // Repo_Student()
    //======================================
    public Repo_Student() {
      this.Students = (List<Student>)HttpContext.Current.Application["Students"];
    }


    // T

    //======================================
    // toListOfStudentBase()
    //======================================
    public List<StudentBase> toListOfStudentBase(List<Models.Student> students) {
      List<StudentBase> lsb = new List<StudentBase>();

      foreach (var item in students) {
        StudentBase sb = new StudentBase();
        sb.StudentId = item.Id;
        sb.StudentNumber = item.StudentNumber;
        lsb.Add(sb);
      }
      return lsb;
    }


  }
}