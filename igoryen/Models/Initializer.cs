// 10. defines UserManager, RoleManager
// 20. identity role
using igoryen.Models;
using Microsoft.AspNet.Identity; // 10
using Microsoft.AspNet.Identity.EntityFramework; // 20
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace igoryen.Models {
    public class Initializer : DropCreateDatabaseAlways<DataContext> {



        //===================================================
        // InitianlizeIdentityForEF()
        //===================================================
        //private void InitializeIdentityForEF(MyDbContext context) {
        private void InitializeIdentityForEF(DataContext dc) {

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dc));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dc));


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

            var UserIgor = new ApplicationUser(); // 10
            //string userName1 = "Igor"; // 11
            string userPw1 = "123456"; // 12
            var userInfo1 = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" }; // 14
            UserIgor.UserName = "Igor"; // 20
            UserIgor.HomeTown = "Sochi";
            UserIgor.MyUserInfo = userInfo1; // 22
            var UserIgorCreateResult = UserManager.Create(UserIgor, userPw1); // 30
            if (UserIgorCreateResult.Succeeded) { // 35
                var addUserIgorToRole1Result = UserManager.AddToRole(UserIgor.Id, roleName1); // 40 Admin
            }

            // Students - roleName 2
            //===============================================
            var UserBob = new ApplicationUser(); // 10
            string userPw2 = "123456"; // 12
            var userInfo2 = new MyUserInfo() { FirstName = "Bob", LastName = "White" }; // 14
            UserBob.UserName = "Bob"; // 20
            UserBob.HomeTown = "Sochi";
            UserBob.MyUserInfo = userInfo2; // 22
            var UserBobCreate = UserManager.Create(UserBob, userPw2); // 30
            if (UserBobCreate.Succeeded) { // 35
                var addUserBobToRole2Result = UserManager.AddToRole(UserBob.Id, roleName2); // 40
            }

            //===============================================
            var UserMary = new ApplicationUser(); // 10
            string userPw3 = "123456"; // 12
            var userInfo3 = new MyUserInfo() { FirstName = "Mary", LastName = "Brown" }; // 14
            UserMary.UserName = "Mary"; // 20
            UserMary.HomeTown = "Toronto";
            UserMary.MyUserInfo = userInfo3; // 22
            var UserMaryCreate = UserManager.Create(UserMary, userPw3); // 30
            if (UserMaryCreate.Succeeded) { // 35
                var addUserMaryToRole2Result = UserManager.AddToRole(UserMary.Id, roleName2); // 40
            }

            //===============================================
            var UserWei = new ApplicationUser(); // 10
            string userPw4 = "123456"; // 12
            var userInfo4 = new MyUserInfo() { FirstName = "Wei", LastName = "Chen" }; // 14
            UserWei.UserName = "Wei"; // 20
            UserWei.HomeTown = "Toronto";
            UserWei.MyUserInfo = userInfo4; // 22
            var UserWeiCreate = UserManager.Create(UserWei, userPw4); // 30
            if (UserWeiCreate.Succeeded) { // 35
                var addUserWeiToRole2Result = UserManager.AddToRole(UserWei.Id, roleName2); // 40
            }

            //===============================================
            var UserJohn = new ApplicationUser(); // 10
            string userPw5 = "123456"; // 12
            var userInfo5 = new MyUserInfo() { FirstName = "John", LastName = "Woo" }; // 14
            UserJohn.UserName = "John"; // 20
            UserJohn.HomeTown = "Toronto";
            UserJohn.MyUserInfo = userInfo5; // 22
            var UserJohnCreate = UserManager.Create(UserJohn, userPw5); // 30
            if (UserJohnCreate.Succeeded) { // 35
                var addUserJohnToRole2Result = UserManager.AddToRole(UserJohn.Id, roleName2); // 40
            }

            //===============================================
            var UserJack = new ApplicationUser(); // 10
            string userPw6 = "123456"; // 12
            var userInfo6 = new MyUserInfo() { FirstName = "Jack", LastName = "Smith" }; // 14
            UserJack.UserName = "Jack"; // 20
            UserJack.HomeTown = "Toronto";
            UserJack.MyUserInfo = userInfo6; // 22
            var UserJackCreate = UserManager.Create(UserJack, userPw6); // 30
            if (UserJackCreate.Succeeded) { // 35
                var addUserJackToRole2Result = UserManager.AddToRole(UserJack.Id, roleName2); // 40
            }

            //===============================================
            var UserJill = new ApplicationUser(); // 10
            string userPw7 = "123456"; // 12
            var userInfo7 = new MyUserInfo() { FirstName = "Jill", LastName = "Smith" }; // 14
            UserJill.UserName = "Jill"; // 20
            UserJill.HomeTown = "Toronto";
            UserJill.MyUserInfo = userInfo7; // 22
            var UserJillCreate = UserManager.Create(UserJill, userPw7); // 30
            if (UserJillCreate.Succeeded) { // 35
                var addUserJillToRole2Result = UserManager.AddToRole(UserJill.Id, roleName2); // 40
            }

            //Faculties - Rolename 3

            //===============================================
            var UserPeter = new ApplicationUser(); // 10
            string userPw8 = "123456"; // 12
            var userInfo8 = new MyUserInfo() { FirstName = "Peter", LastName = "Peterson" }; // 14
            UserPeter.UserName = "Peter"; // IdentityUser.UserName (string)
            UserPeter.HomeTown = "Toronto";
            UserPeter.MyUserInfo = userInfo8; // 22
            var UserPeterCreate = UserManager.Create(UserPeter, userPw8); // 30
            if (UserPeterCreate.Succeeded) { // 35
                var addUserPeterToRole3Result = UserManager.AddToRole(UserPeter.Id, roleName3); // 40
            }

            //===============================================
            var UserAdam = new ApplicationUser(); // 10
            string userPw9 = "123456"; // 12
            var userInfo9 = new MyUserInfo() { FirstName = "Adam", LastName = "Adamson" }; // 14
            UserAdam.UserName = "Adam"; // 20
            UserAdam.HomeTown = "Toronto";
            UserAdam.MyUserInfo = userInfo9; // 22
            var UserAdamCreate = UserManager.Create(UserAdam, userPw9); // 30
            if (UserAdamCreate.Succeeded) { // 35
                var addUserAdamToRole3Result = UserManager.AddToRole(UserAdam.Id, roleName3); // 40
            }

            //===============================================
            var UserRon = new ApplicationUser(); // 10
            string userPw10 = "123456"; // 12
            var userInfo10 = new MyUserInfo() { FirstName = "Ronald", LastName = "Ronaldson" }; // 14
            UserRon.UserName = "Ron"; // 20
            UserRon.HomeTown = "Toronto";
            UserRon.MyUserInfo = userInfo3; // 22
            var UserRonCreate = UserManager.Create(UserRon, userPw10); // 30
            if (UserRonCreate.Succeeded) { // 35
                var addUserRonToRole3Result = UserManager.AddToRole(UserRon.Id, roleName3); // 40
            }

            //===============================================
            var UserBill = new ApplicationUser(); // 10
            string userPw11 = "123456"; // 12
            var userInfo11 = new MyUserInfo() { FirstName = "Bill", LastName = "Johnson" }; // 14
            UserBill.UserName = "Bill"; // 20
            UserBill.HomeTown = "Toronto";
            UserBill.MyUserInfo = userInfo11; // 22
            var UserBillCreate = UserManager.Create(UserBill, userPw11); // 30
            if (UserBillCreate.Succeeded) { // 35
                var addUserBillToRole3Result = UserManager.AddToRole(UserBill.Id, roleName3); // 40
            }

            // Initialize tables


            try {

                //-------------------------------------
                // initialize a few ComMethod rows
                //-------------------------------------


                /*
                ComMethod bobby = new ComMethod();
                bobby.ComMethodId = 1;
                bobby.Handle = "bobby";
                bobby.CellNo = "647-111-2222";
                bobby.Email = "bobby@gmail.com";
                bob.ComMethods.Add(bobby);

                ComMethod marie = new ComMethod();
                marie.ComMethodId = 1;
                marie.Handle = "marie";
                marie.CellNo = "647-111-3333";
                marie.Email = "marie@gmail.com";
                mary.ComMethods.Add(marie);

                ComMethod way = new ComMethod();
                way.ComMethodId = 1;
                way.Handle = "way";
                way.CellNo = "647-111-4444";
                way.Email = "way@gmail.com";
                wei.ComMethods.Add(way);

                ComMethod johnny = new ComMethod();
                johnny.ComMethodId = 1;
                johnny.Handle = "johnny";
                johnny.CellNo = "647-111-5555";
                johnny.Email = "johnny@gmail.com";
                john.ComMethods.Add(johnny);

                ComMethod jackie = new ComMethod();
                jackie.ComMethodId = 1;
                jackie.Handle = "jackie";
                jackie.CellNo = "647-111-6666";
                jackie.Email = "jackie@gmail.com";
                jack.ComMethods.Add(jackie);

                ComMethod gill = new ComMethod();
                gill.ComMethodId = 1;
                gill.Handle = "gill";
                gill.CellNo = "647-111-7777";
                gill.Email = "gill@gmail.com";
                jill.ComMethods.Add(gill);
                */

                /*
                 * 1a initialize students
                 * 1b initialize faculties
                 * 1c initialize courses
                 * 
                 * 2a each course gets 1 faculty
                 * 2b each course gets n students
                 * 
                 * 3 each faculty gets n courses
                 * 
                 * 4 each student gets n courses
                 */

                // 1a) initialize students
                // bob
                Student bob = new Student(); // 10
                bob.PersonId = 1; // 20
                bob.FirstName = "Bob";
                bob.LastName = "White";
                bob.Phone = "555-555-5555";
                bob.SenecaId = "011111111";
                bob.UserName = "Bob_";
                //bob.UserName = UserBob.UserName;
                dc.Students.Add(bob);
                // mary
                Student mary = new Student();
                mary.PersonId = 2;
                mary.FirstName = "Mary";
                mary.LastName = "Brown";
                mary.Phone = "555-555-5555";
                mary.SenecaId = "011111112";
                mary.UserName = "Mary_";
                //mary.UserName = user3.UserName;
                dc.Students.Add(mary);
                // wei
                Student wei = new Student();
                wei.PersonId = 3;
                wei.FirstName = "Wei";
                wei.LastName = "Chen";
                wei.Phone = "555-555-5555";
                wei.SenecaId = "011111113";
                wei.UserName = "Wei_";
                //wei.UserName = UserWei.UserName;
                dc.Students.Add(wei);
                // john
                Student john = new Student("John", "Woo", "555-555-1234", "011111114");
                john.PersonId = 4;
                john.UserName = "John_";
                dc.Students.Add(john);
                // jack
                Student jack = new Student("Jack", "Smith", "555-555-1235", "011111115");
                jack.PersonId = 5;
                jack.UserName = "Jack_";
                dc.Students.Add(jack);
                // jill
                Student jill = new Student("Jill", "Smith", "555-555-1236", "011111116");
                jill.PersonId = 6;
                jill.UserName = "Jill_";
                dc.Students.Add(jill);

                //dc.SaveChanges();


                //===============================================
                //===============================================
                // create faculty Peter
                Faculty f = new Faculty("Peter", "Peterson", "555-567-6789", "034234678"); // 20
                f.PersonId = 10; // 25
                f.UserName = "Peter_";
                dc.Faculties.Add(f);

                //==============================================
                // 1) Peter teaches IPC144
                Course c = new Course();
                c.CourseCode = "IPC144";
                c.CourseName = "Introduction into programming";
                c.RoomNumber = "1000";  
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserPeter; // 17
                f.Courses.Add(c); // <------------------------
                c.Students.Add(bob);
                bob.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;
                //=====================================================
                // 2) Peter teaches ULI101
                c = new Course();
                c.CourseCode = "ULI101";
                c.CourseName = "OS - Unix";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserPeter;
                c.Students.Add(mary);
                mary.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;
                //=====================================================
                // 3) Peter teaches IOS110
                c = new Course();
                c.CourseCode = "IOS110";
                c.CourseName = "OS - Windows";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserPeter;
                c.Students.Add(wei);
                wei.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //=====================================================
                // 4) Peter teaches OOP244
                c = new Course();
                c.CourseCode = "OOP244";
                c.CourseName = "OOP development using C++";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserPeter;
                c.Students.Add(bob);
                bob.Courses.Add(c);
                c.Students.Add(john);
                john.Courses = new List<Course>();
                john.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;
                f = null;

                //====================================
                //====================================
                // create faculty Adam
                f = new Faculty("Adam", "Adamson", "555-567-6790", "034234677");
                f.PersonId = 11;
                f.UserName = "Adam_";
                dc.Faculties.Add(f); // 35

                //===================================
                // 1) Adam teaches INT222
                c = new Course();
                c.CourseCode = "INT222";
                c.CourseName = "Web development - client";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserAdam;
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses = new List<Course>();
                jack.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 2) Adam teaches IBC233
                c = new Course();
                c.CourseCode = "IBC233";
                c.CourseName = "iSeries - Business Applications";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserAdam;
                c.Students.Add(wei);
                wei.Courses.Add(c);
                c.Students.Add(jill);
                jill.Courses = new List<Course>();
                jill.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 3) Adam teaches DBS201
                c = new Course();
                c.CourseCode = "DBS201";
                c.CourseName = "Database principles";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserAdam;
                c.Students.Add(bob);
                bob.Courses.Add(c);
                c.Students.Add(john);
                john.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 4) Adam teaches OOP344
                c = new Course();
                c.CourseCode = "OOP344";
                c.CourseName = "OOP development - C++";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserAdam;
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                f = null;


                //====================================
                //====================================
                // create faulty Ron
                f = new Faculty("Ronald", "Ronaldson", "555-567-6791", "034234676");
                f.PersonId = 12;
                f.UserName = "Ron_";
                dc.Faculties.Add(f); // 35

                //===================================
                // 1) Ron teaches INT322
                c = new Course();
                c.CourseCode = "INT322";
                c.CourseName = "Web development - Unix server";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserRon;
                c.Students.Add(wei);
                wei.Courses.Add(c);
                c.Students.Add(jill);
                jill.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 2) Ron teaches DBS301
                c = new Course();
                c.CourseCode = "DBS301";
                c.CourseName = "Database design and development";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserRon;
                c.Students.Add(jill);
                jill.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 3) Ron teaches JAC444
                c = new Course();
                c.CourseCode = "JAC444";
                c.CourseName = "OOP develoment - Java";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserRon;
                c.Students.Add(bob);
                bob.Courses.Add(c);
                c.Students.Add(john);
                john.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 4) Ron teaches INT422

                c = new Course();
                c.CourseCode = "INT422";
                c.CourseName = "Web development - Wondows";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserRon;
                c.Students.Add(bob);
                bob.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                f = null;

                //====================================
                //====================================
                // create faculty Bill
                f = new Faculty("Bill", "Johnson", "555-567-6792", "034234677");
                f.PersonId = 13;
                f.UserName = "Bill_";
                dc.Faculties.Add(f); // 35

                //===================================
                // 1) Bill teaches DCN455
                c = new Course();
                c.CourseCode = "DCN455";
                c.CourseName = "Data communication for developers";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserBill;
                c.Students.Add(mary);
                mary.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 2) Bill teaches BAC344
                c = new Course();
                c.CourseCode = "BAC344";
                c.CourseName = "Business apps - Cobol";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserBill;
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 3) Bill teaches MAP524
                c = new Course();
                c.CourseCode = "MAP524";
                c.CourseName = "Mobile apps - Android";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserBill;
                c.Students.Add(wei);
                wei.Courses.Add(c);
                c.Students.Add(jill);
                jill.Courses.Add(c);
                dc.Courses.Add(c);
                c = null;

                //===================================
                // 4) Bill teaches  WIN210
                c = new Course();
                c.CourseCode = "WIN210";
                c.CourseName = "Windows administration";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.User = UserBill;
                c.Students.Add(john);
                john.Courses.Add(c);
                dc.Courses.Add(c);


                dc.SaveChanges();

                // 1c) initialize courses



                //14

                //15

                //16


                //dc.SaveChanges();

                // 2a) each course gets 1 faculty

                // Courses taught by Peter

                //ipc144.Faculty = new Faculty();
                //ipc144.Faculty = peter;
                //dc.Courses.Add(ipc144);

                //uli101.Faculty = new Faculty();
                //uli101.Faculty = peter;
                //dc.Courses.Add(uli101);

                //ios110.Faculty = new Faculty();
                //ios110.Faculty = peter;
                //dc.Courses.Add(ios110);

                //oop244.Faculty = new Faculty();
                //oop244.Faculty = peter;
                //dc.Courses.Add(oop244);

                //// Courses taught by Adam

                //int222.Faculty = new Faculty();
                //int222.Faculty = adam;
                //dc.Courses.Add(int222);

                //ibc233.Faculty = new Faculty();
                //ibc233.Faculty = adam;
                //dc.Courses.Add(ibc233);

                //dbs201.Faculty = new Faculty();
                //dbs201.Faculty = adam;
                //dc.Courses.Add(dbs201);

                //oop344.Faculty = new Faculty();
                //oop344.Faculty = adam;
                //dc.Courses.Add(oop344);

                //// Courses taught by Ron

                //int322.Faculty = new Faculty();
                //int322.Faculty = ron;
                //dc.Courses.Add(int322);

                //dbs301.Faculty = new Faculty();
                //dbs301.Faculty = ron;
                //dc.Courses.Add(dbs301);

                //int422.Faculty = new Faculty();
                //int422.Faculty = ron;
                //dc.Courses.Add(int422);

                //// Courses taught by Bill

                //dcn455.Faculty = new Faculty();
                //dcn455.Faculty = bill;
                //dc.Courses.Add(dcn455);

                //bac344.Faculty = new Faculty();
                //bac344.Faculty = bill;
                //dc.Courses.Add(bac344);

                //map524.Faculty = new Faculty();
                //map524.Faculty = bill;
                //dc.Courses.Add(map524);

                //win210.Faculty = new Faculty();
                //win210.Faculty = bill;
                //dc.Courses.Add(win210);

                //dc.SaveChanges();

                //// 2b) each course gets n students
                ////1
                //ipc144.Students = new List<Student>();
                //ipc144.Students.Add(bob);
                //dc.Courses.Add(ipc144);
                ////2
                //uli101.Students = new List<Student>();
                //uli101.Students.Add(mary);
                //dc.Courses.Add(uli101);
                ////3
                //ios110.Students = new List<Student>();
                //ios110.Students.Add(wei);
                //dc.Courses.Add(ios110);
                ////4
                //oop244.Students = new List<Student>();
                //oop244.Students.Add(bob);
                //oop244.Students.Add(john);
                //dc.Courses.Add(oop244);
                ////5
                //int222.Students = new List<Student>();
                //int222.Students.Add(mary);
                //int222.Students.Add(jack);
                //dc.Courses.Add(int222);
                ////6
                //ibc233.Students = new List<Student>();
                //ibc233.Students.Add(wei);
                //ibc233.Students.Add(jill);
                //dc.Courses.Add(ibc233);
                ////7
                //dbs201.Students = new List<Student>();
                //dbs201.Students.Add(bob);
                //dbs201.Students.Add(john);
                //dc.Courses.Add(dbs201);
                ////8
                //oop344.Students = new List<Student>();
                //oop344.Students.Add(mary);
                //oop344.Students.Add(jack);
                //dc.Courses.Add(oop344);
                ////9
                //int322.Students = new List<Student>();
                //int322.Students.Add(wei);
                //int322.Students.Add(jill);
                //dc.Courses.Add(int322);
                ////10
                //dbs301.Students = new List<Student>();
                //dbs301.Students.Add(jill);
                //dc.Courses.Add(dbs301);
                ////11
                //jac444.Students = new List<Student>();
                //jac444.Students.Add(bob);
                //jac444.Students.Add(jack);
                //jac444.Students.Add(john);
                //dc.Courses.Add(jac444);
                ////12
                //// no one is taking int422

                ////13
                //// no one is taking dcn455

                ////14
                //bac344.Students = new List<Student>();
                //bac344.Students.Add(mary);
                //bac344.Students.Add(jack);
                //dc.Courses.Add(bac344);
                ////15
                //map524.Students = new List<Student>();
                //map524.Students.Add(wei);
                //map524.Students.Add(jill);
                //dc.Courses.Add(map524);
                ////16
                //win210.Students = new List<Student>();
                //win210.Students.Add(john);
                //dc.Courses.Add(win210);

                //dc.SaveChanges();

                // 3) each faculty gets n courses

                //peter.Courses = new List<Course>();
                //peter.Courses.Add(ipc144);
                //dc.Faculties.Add(peter);
                //dc.SaveChanges();

                //peter.Courses.Add(uli101);
                //dc.Faculties.Add(peter);
                //dc.SaveChanges();

                //peter.Courses.Add(ios110);
                //dc.Faculties.Add(peter);
                //dc.SaveChanges();

                //peter.Courses.Add(oop244);
                //dc.Faculties.Add(peter);
                //dc.SaveChanges();

                //adam.Courses = new List<Course>();
                //adam.Courses.Add(int222);
                //adam.Courses.Add(ibc233);
                //adam.Courses.Add(dbs201);
                //adam.Courses.Add(oop344);
                //dc.Faculties.Add(adam);

                //dc.SaveChanges();


                //bill.Courses = new List<Course>();
                //bill.Courses.Add(dcn455);
                //bill.Courses.Add(bac344);
                //bill.Courses.Add(map524);
                //bill.Courses.Add(win210);
                //dc.Faculties.Add(bill);

                //dc.SaveChanges();


                //ron.Courses = new List<Course>();
                //ron.Courses.Add(int322);
                //ron.Courses.Add(dbs301);
                //ron.Courses.Add(jac444);
                //ron.Courses.Add(int422);
                //dc.Faculties.Add(ron);

                //dc.SaveChanges();

                //// 4) each student gets n courses

                //bob.Courses = new List<Course>();
                //bob.Courses.Add(ipc144);
                //bob.Courses.Add(oop244);
                //bob.Courses.Add(dbs201);
                //bob.Courses.Add(jac444);
                //dc.Students.Add(bob);

                //mary.Courses = new List<Course>();
                //mary.Courses.Add(uli101);
                //mary.Courses.Add(int222);
                //mary.Courses.Add(oop344);
                //mary.Courses.Add(bac344);
                //dc.Students.Add(mary);

                //wei.Courses = new List<Course>();
                //wei.Courses.Add(ios110);
                //wei.Courses.Add(ibc233);
                //wei.Courses.Add(int322);
                //wei.Courses.Add(map524);
                //dc.Students.Add(wei);

                //john.Courses = new List<Course>();
                //john.Courses.Add(oop244);
                //john.Courses.Add(dbs201);
                //john.Courses.Add(jac444);
                //john.Courses.Add(win210);
                //dc.Students.Add(john);

                //jack.Courses = new List<Course>();
                //jack.Courses.Add(int222);
                //jack.Courses.Add(oop344);
                //jack.Courses.Add(jac444);
                //jack.Courses.Add(bac344);
                //dc.Students.Add(jack);

                //jill.Courses = new List<Course>();
                //jill.Courses.Add(ibc233);
                //jill.Courses.Add(int322);
                //jill.Courses.Add(dbs301);
                //jill.Courses.Add(map524);
                //dc.Students.Add(jill);

                dc.SaveChanges(); // commit changes

                var course = dc.Courses;

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
            }

        }

        protected override void Seed(DataContext dc) {
            InitializeIdentityForEF(dc);
            base.Seed(dc);
        }


        public List<Student> Students { get; set; }
        public List<Faculty> Faculties { get; set; }
        public List<Course> Courses { get; set; }

    }
}