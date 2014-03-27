// 10. defines UserManager, RoleManager
// 20. identity role
using igoryen.Models;
using Microsoft.AspNet.Identity; // 10
using Microsoft.AspNet.Identity.EntityFramework; // 20
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace igoryen.Models {
  public class Initializer : DropCreateDatabaseAlways<DataContext> {

    //===================================================
    // InitializeTables()
    //===================================================
    private void InitializeTables(DataContext dc){
      //-------------------------------------
      // #1 - initialize a few student rows
      // 10. create a student row
      // 20. assign all the basic info to the student
      // 30. assign 1/more courses to the student
      // 40. add the student row to table Students
      // 50. add the student row to table Students of course 1
      // 55. add the student row to table Students in course 2
      // 60. purge student row
      //-------------------------------------
      Student bob = new Student(); // 10
      bob.Id = 1; // 20
      bob.FirstName = "Bob";
      bob.LastName = "White";
      bob.Phone = "555-555-5555";
      bob.StudentNumber = "011111111";
      dc.Students.Add(bob);

      Student mary = new Student();
      mary.Id = 2;
      mary.FirstName = "Mary";
      mary.LastName = "Brown";
      mary.Phone = "555-555-5555";
      mary.StudentNumber = "011111112";
      dc.Students.Add(mary);

      Student wei = new Student();
      wei.Id = 3;
      wei.FirstName = "Wei";
      wei.LastName = "Chen";
      wei.Phone = "555-555-5555";
      wei.StudentNumber = "011111113";
      dc.Students.Add(wei);

      Student john = new Student("John", "Woo", "555-555-1234", "011111114");
      john.Id = 4;
      dc.Students.Add(john);

      Student jack = new Student("Jack", "Smith", "555-555-1235", "011111115");
      jack.Id = 5;
      dc.Students.Add(jack);

      Student jill = new Student("Jill", "Smith", "555-555-1236", "011111116");
      jack.Id = 6;
      dc.Students.Add(jill);
      
      //-------------------------------------
      // initialize a few ComMethod rows
      //-------------------------------------


      //-------------------------------------
      // #2 - initialize a few course rows
      // 60. insert rows into table Courses
      //-------------------------------------
      // 1
      Course ipc144 = new Course();
      ipc144.CourseCode = "IPC144";
      ipc144.CourseName = "Introduction into programming";
      dc.Courses.Add(ipc144);
      //2
      Course uli101 = new Course();
      uli101.CourseCode = "ULI101";
      uli101.CourseName = "OS - Unix";
      dc.Courses.Add(uli101);
      //3
      Course ios110 = new Course();
      ios110.CourseCode = "IOS110";
      ios110.CourseName = "OS - Windows";
      dc.Courses.Add(ios110);
      //4
      Course oop244 = new Course();
      oop244.CourseCode = "OOP244";
      oop244.CourseName = "OOP development using C++";
      dc.Courses.Add(oop244);
      //5
      Course int222 = new Course();
      int222.CourseCode = "INT222";
      int222.CourseName = "Web development - client";
      dc.Courses.Add(int222);
      //6
      Course ibc233 = new Course();
      ibc233.CourseCode = "IBC233";
      ibc233.CourseName = "Russian Basics";
      dc.Courses.Add(ibc233);
      //7
      Course dbs201 = new Course();
      dbs201.CourseName = "Database principles";
      dbs201.CourseCode = "DBS201";
      dc.Courses.Add(dbs201);
      //8
      Course oop344 = new Course();
      oop344.CourseCode = "OOP344";
      oop344.CourseName = "OOP development - C++";
      dc.Courses.Add(oop344);
      //9
      Course int322 = new Course();
      int322.CourseCode = "INT322";
      int322.CourseName = "Web development - Unix server";
      dc.Courses.Add(int322);
      //10
      Course dbs301 = new Course();
      dbs301.CourseCode = "DBS301";
      dbs301.CourseName = "Database design and development";
      dc.Courses.Add(dbs301);
      //11
      Course jac444 = new Course();
      jac444.CourseCode = "JAC444";
      jac444.CourseName = "OOP develoment - Java";
      dc.Courses.Add(jac444);
      //12
      Course int422 = new Course();
      int422.CourseCode = "INT422";
      int422.CourseName = "Russian Basics";
      dc.Courses.Add(int422);
      //13
      Course dcn455 = new Course();
      dcn455.CourseCode = "DCN455";
      dcn455.CourseName = "Data communication for developers";
      dc.Courses.Add(dcn455);
      //14
      Course bac344 = new Course();
      bac344.CourseCode = "BAC344";
      bac344.CourseName = "Business apps - Cobol";
      dc.Courses.Add(bac344);
      //15
      Course map524 = new Course();
      map524.CourseCode = "MAP524";
      map524.CourseName = "Mobile apps - Android";
      dc.Courses.Add(map524);
      //16
      Course win210 = new Course();
      win210.CourseCode = "WIN210";
      win210.CourseName = "Windows administration";
      dc.Courses.Add(win210);

      // 60
      //-------------------------------------
      // #3 - assign a set of courses to each student
      //-------------------------------------
      bob.Courses = new List<Course>();
      bob.Courses.Add(ipc144);
      bob.Courses.Add(oop244);
      bob.Courses.Add(dbs201);
      bob.Courses.Add(jac444);
      dc.Students.Add(bob);

      mary.Courses = new List<Course>();
      mary.Courses.Add(uli101);
      mary.Courses.Add(int222);
      mary.Courses.Add(oop344);
      mary.Courses.Add(bac344);
      dc.Students.Add(mary);

      wei.Courses = new List<Course>();
      wei.Courses.Add(ios110);
      wei.Courses.Add(ibc233);
      wei.Courses.Add(int322);
      wei.Courses.Add(map524);
      dc.Students.Add(wei);

      john.Courses = new List<Course>();
      john.Courses.Add(oop244);
      john.Courses.Add(dbs201);
      john.Courses.Add(jac444);
      john.Courses.Add(win210);
      dc.Students.Add(john);

      jack.Courses = new List<Course>();
      jack.Courses.Add(int222);
      jack.Courses.Add(oop344);
      jack.Courses.Add(jac444);
      jack.Courses.Add(bac344);
      dc.Students.Add(jack);

      jill.Courses = new List<Course>();
      jill.Courses.Add(ibc233);
      jill.Courses.Add(int322);
      jill.Courses.Add(dbs301);
      jill.Courses.Add(map524);
      dc.Students.Add(jill);
      
      // #4 - assign a set of students to each course

      ipc144.Students = new List<Student>();
      ipc144.Students.Add(bob);
      dc.Courses.Add(ipc144);

      uli101.Students = new List<Student>();
      uli101.Students.Add(mary);
      dc.Courses.Add(uli101);

      ios110.Students = new List<Student>();
      uli101.Students.Add(wei);
      dc.Courses.Add(ios110);

      oop244.Students = new List<Student>();
      oop244.Students.Add(bob);
      oop244.Students.Add(john);
      dc.Courses.Add(oop244);

      int222.Students = new List<Student>();
      int222.Students.Add(mary);
      int222.Students.Add(jack);
      dc.Courses.Add(int222);

      ibc233.Students = new List<Student>();
      ibc233.Students.Add(wei);
      ibc233.Students.Add(jill);
      dc.Courses.Add(ibc233);

      dbs201.Students = new List<Student>();
      dbs201.Students.Add(bob);
      dbs201.Students.Add(john);
      dc.Courses.Add(dbs201);

      oop344.Students = new List<Student>();
      oop344.Students.Add(mary);
      oop344.Students.Add(jack);
      dc.Courses.Add(oop344);

      int322.Students = new List<Student>();
      int322.Students.Add(wei);
      int322.Students.Add(jill);
      dc.Courses.Add(int322);

      dbs301.Students = new List<Student>();
      dbs301.Students.Add(jill);
      dc.Courses.Add(dbs301);

      jac444.Students = new List<Student>();
      jac444.Students.Add(bob);
      jac444.Students.Add(jack);
      jac444.Students.Add(john);
      dc.Courses.Add(jac444);

      bac344.Students = new List<Student>();
      bac344.Students.Add(mary);
      bac344.Students.Add(jack);
      dc.Courses.Add(bac344);

      map524.Students = new List<Student>();
      map524.Students.Add(wei);
      map524.Students.Add(jill);
      dc.Courses.Add(map524);

      win210.Students = new List<Student>();
      win210.Students.Add(john);
      dc.Courses.Add(win210);

      //-------------------------------------
      // #5 - initialize some faculties and assign to them courses
      //-------------------------------------
      Faculty peter = new Faculty("Peter", "Peterson", "555-567-6789", "034234678"); // 20
      peter.Id = 10; // 25
      peter.Courses.Add(ipc144);
      peter.Courses.Add(uli101);
      peter.Courses.Add(ios110);
      peter.Courses.Add(oop244);
      dc.Faculties.Add(peter);

      Faculty adam = new Faculty("Adam", "Adamson", "555-567-6790", "034234677");
      adam.Id = 11;
      adam.Courses.Add(int222);
      adam.Courses.Add(ibc233);
      adam.Courses.Add(dbs201);
      adam.Courses.Add(oop344);
      dc.Faculties.Add(adam); // 35

      Faculty ron = new Faculty("Ronald", "Ronaldson", "555-567-6791", "034234676");
      ron.Id = 12;
      ron.Courses.Add(int322);
      ron.Courses.Add(dbs301);
      ron.Courses.Add(jac444);
      ron.Courses.Add(int422);
      dc.Faculties.Add(ron); // 35

      Faculty bill = new Faculty("Bill", "Johnson", "555-567-6792", "034234677");
      bill.Id = 13;
      bill.Courses.Add(dcn455);
      bill.Courses.Add(bac344);
      bill.Courses.Add(map524);
      bill.Courses.Add(win210);
      dc.Faculties.Add(bill); // 35

      //dc.Faculty.Add(fac);

      dc.SaveChanges(); // commit changes
    }

    //===================================================
    // InitianlizeIdentityForEF()
    //===================================================
    //private void InitializeIdentityForEF(MyDbContext context) {
    private void InitializeIdentityForEF(DataContext context) {

      var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
      var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

      
      //================================================
      // *** CREATE ROLES SECTION ***
      // 03. set up the role's name
      // 05. check if a role with this name exists
      // 06. if such a role doesn't exist, create it
      //================================================
      string roleName1 = "Admin"; // 3
      if (!RoleManager.RoleExists(roleName1)) { // 5
        var role1CreateResult = RoleManager.Create(new IdentityRole(roleName1)); // 6
      }

      string roleName2 = "Student";
      if (!RoleManager.RoleExists(roleName2)) {
        var role2CreateResult = RoleManager.Create(new IdentityRole(roleName2));
      }

      string roleName3 = "Faculty";
      if (!RoleManager.RoleExists(roleName3)) {
        var role3CreateResult = RoleManager.Create(new IdentityRole(roleName3));
      }
      //================================================
      // *** CREATE APP USERS SECTION ***
      // 10. set up an app user
      // 11. set up a nickname for the user
      // 12. set up the user password
      // 14. fill out the user's info
      // 20. assign a nick name to the user
      // 22. assign the user's filled out info
      // 30. attempt to create the app user with his pw
      // 35. if succeeded to create user
      // 40. add the app user to role "Admin"
      //================================================
     
      // Admin

      var user1 = new ApplicationUser(); // 10
      //string userName1 = "Igor"; // 11
      string userPw1 = "123456"; // 12
      var userInfo1 = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" }; // 14
      user1.UserName = "Igor"; // 20
      user1.HomeTown = "Sochi";
      user1.MyUserInfo = userInfo1; // 22
      var user1CreateResult = UserManager.Create(user1, userPw1); // 30
      if (user1CreateResult.Succeeded) { // 35
        var addUser1ToRole1Result = UserManager.AddToRole(user1.Id, roleName1); // 40 Admin
      }

      // Students - roleName 2
      //===============================================
      var user2 = new ApplicationUser(); // 10
      string userPw2 = "123456"; // 12
      var userInfo2 = new MyUserInfo() { FirstName = "Bob", LastName = "White" }; // 14
      user2.UserName = "Bob"; // 20
      user2.HomeTown = "Sochi";
      user2.MyUserInfo = userInfo2; // 22
      var user2Create = UserManager.Create(user2, userPw2); // 30
      if (user2Create.Succeeded) { // 35
        var addUser2ToRole2Result = UserManager.AddToRole(user2.Id, roleName2); // 40
      }

      //===============================================
      var user3 = new ApplicationUser(); // 10
      string userPw3 = "123456"; // 12
      var userInfo3 = new MyUserInfo() { FirstName = "Mary", LastName = "Brown" }; // 14
      user3.UserName = "Mary"; // 20
      user3.HomeTown = "Toronto";
      user3.MyUserInfo = userInfo3; // 22
      var user3Create = UserManager.Create(user3, userPw3); // 30
      if (user3Create.Succeeded) { // 35
        var addUser3ToRole2Result = UserManager.AddToRole(user3.Id, roleName2); // 40
      }

      //===============================================
      var user4 = new ApplicationUser(); // 10
      string userPw4 = "123456"; // 12
      var userInfo4 = new MyUserInfo() { FirstName = "Wei", LastName = "Chen" }; // 14
      user4.UserName = "Wei"; // 20
      user4.HomeTown = "Toronto";
      user4.MyUserInfo = userInfo4; // 22
      var user4Create = UserManager.Create(user4, userPw4); // 30
      if (user4Create.Succeeded) { // 35
        var addUser4ToRole2Result = UserManager.AddToRole(user4.Id, roleName2); // 40
      }

      //===============================================
      var user5 = new ApplicationUser(); // 10
      string userPw5 = "123456"; // 12
      var userInfo5 = new MyUserInfo() { FirstName = "John", LastName = "Woo" }; // 14
      user5.UserName = "John"; // 20
      user5.HomeTown = "Toronto";
      user5.MyUserInfo = userInfo5; // 22
      var user5Create = UserManager.Create(user5, userPw5); // 30
      if (user5Create.Succeeded) { // 35
        var addUser5ToRole2Result = UserManager.AddToRole(user5.Id, roleName2); // 40
      }

      //===============================================
      var user6 = new ApplicationUser(); // 10
      string userPw6 = "123456"; // 12
      var userInfo6 = new MyUserInfo() { FirstName = "Jack", LastName = "Smith" }; // 14
      user6.UserName = "Jack"; // 20
      user6.HomeTown = "Toronto";
      user6.MyUserInfo = userInfo6; // 22
      var user6Create = UserManager.Create(user6, userPw6); // 30
      if (user6Create.Succeeded) { // 35
        var addUser6ToRole2Result = UserManager.AddToRole(user6.Id, roleName2); // 40
      }

      //===============================================
      var user7 = new ApplicationUser(); // 10
      string userPw7 = "123456"; // 12
      var userInfo7 = new MyUserInfo() { FirstName = "Jill", LastName = "Smith" }; // 14
      user7.UserName = "Jill"; // 20
      user7.HomeTown = "Toronto";
      user7.MyUserInfo = userInfo7; // 22
      var user7Create = UserManager.Create(user7, userPw7); // 30
      if (user7Create.Succeeded) { // 35
        var addUser7ToRole2Result = UserManager.AddToRole(user7.Id, roleName2); // 40
      }

      //Faculties - Rolename 3

      //===============================================
      var user8 = new ApplicationUser(); // 10
      string userPw8 = "123456"; // 12
      var userInfo8 = new MyUserInfo() { FirstName = "Peter", LastName = "Peterson" }; // 14
      user8.UserName = "Peter"; // 20
      user8.HomeTown = "Toronto";
      user8.MyUserInfo = userInfo8; // 22
      var user8Create = UserManager.Create(user8, userPw8); // 30
      if (user8Create.Succeeded) { // 35
        var addUser8ToRole3Result = UserManager.AddToRole(user8.Id, roleName3); // 40
      }

      //===============================================
      var user9 = new ApplicationUser(); // 10
      string userPw9 = "123456"; // 12
      var userInfo9 = new MyUserInfo() { FirstName = "Adam", LastName = "Adamson" }; // 14
      user9.UserName = "Adam"; // 20
      user9.HomeTown = "Toronto";
      user9.MyUserInfo = userInfo9; // 22
      var user9Create = UserManager.Create(user9, userPw9); // 30
      if (user9Create.Succeeded) { // 35
        var addUser9ToRole3Result = UserManager.AddToRole(user9.Id, roleName3); // 40
      }

      //===============================================
      var user10 = new ApplicationUser(); // 10
      string userPw10 = "123456"; // 12
      var userInfo10 = new MyUserInfo() { FirstName = "Ronald", LastName = "Ronaldson" }; // 14
      user10.UserName = "Ron"; // 20
      user10.HomeTown = "Toronto";
      user10.MyUserInfo = userInfo3; // 22
      var user10Create = UserManager.Create(user10, userPw10); // 30
      if (user10Create.Succeeded) { // 35
        var addUser10ToRole3Result = UserManager.AddToRole(user10.Id, roleName3); // 40
      }

      //===============================================
      var user11 = new ApplicationUser(); // 10
      string userPw11 = "123456"; // 12
      var userInfo11 = new MyUserInfo() { FirstName = "Bill", LastName = "Johnson" }; // 14
      user11.UserName = "Bill"; // 20
      user11.HomeTown = "Toronto";
      user11.MyUserInfo = userInfo11; // 22
      var user11Create = UserManager.Create(user11, userPw11); // 30
      if (user11Create.Succeeded) { // 35
        var addUser11ToRole3Result = UserManager.AddToRole(user11.Id, roleName3); // 40
      }

    }

    protected override void Seed(DataContext dc) {
      InitializeIdentityForEF(dc);
      InitializeTables(dc);
      base.Seed(dc);
    }


    public List<Student> Students { get; set; }
    public List<Faculty> Faculties { get; set; }
    public List<Course> Courses { get; set; }

  }
}