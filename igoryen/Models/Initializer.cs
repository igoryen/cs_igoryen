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

        //20
        private void InitializeIdentityForEF(DataContext dc) {

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dc));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dc));

            // create roles
            string roleName1 = "Admin"; // 21
            if (!RoleManager.RoleExists(roleName1)) { // 22
                var role1CreateResult = RoleManager.Create(new IdentityRole(roleName1)); // 23
            }

            string roleName2 = "Student";
            if (!RoleManager.RoleExists(roleName2)) {
                var role2CreateResult = RoleManager.Create(new IdentityRole(roleName2));
            }

            string roleName3 = "Faculty";
            if (!RoleManager.RoleExists(roleName3)) {
                var role3CreateResult = RoleManager.Create(new IdentityRole(roleName3));
            }

            // 24
            // Admin
            var UserIgor = new ApplicationUser(); // 24
            //string userName1 = "Igor"; // 25
            string userPw1 = "123456"; // 26
            var userInfo1 = new MyUserInfo() { FirstName = "Igor", LastName = "Entaltsev" }; // 27
            UserIgor.UserName = "Igor"; // 28
            UserIgor.HomeTown = "Sochi";
            UserIgor.MyUserInfo = userInfo1; // 29
            var UserIgorCreateResult = UserManager.Create(UserIgor, userPw1); // 30
            if (UserIgorCreateResult.Succeeded) { // 31
                var addUserIgorToRole1Result = UserManager.AddToRole(UserIgor.Id, roleName1); // 32 (Admin)
            }

            // Students - roleName 2

            var UserBob = new ApplicationUser();
            string userPw2 = "123456";
            var userInfo2 = new MyUserInfo() { FirstName = "Bob", LastName = "White" };
            UserBob.UserName = "Bob";
            UserBob.HomeTown = "Sochi";
            UserBob.MyUserInfo = userInfo2;
            var UserBobCreate = UserManager.Create(UserBob, userPw2);
            if (UserBobCreate.Succeeded) {
                var addUserBobToRole2Result = UserManager.AddToRole(UserBob.Id, roleName2);
            }

            var UserMary = new ApplicationUser();
            string userPw3 = "123456";
            var userInfo3 = new MyUserInfo() { FirstName = "Mary", LastName = "Brown" };
            UserMary.UserName = "Mary";
            UserMary.HomeTown = "Toronto";
            UserMary.MyUserInfo = userInfo3;
            var UserMaryCreate = UserManager.Create(UserMary, userPw3);
            if (UserMaryCreate.Succeeded) {
                var addUserMaryToRole2Result = UserManager.AddToRole(UserMary.Id, roleName2);
            }

            var UserWei = new ApplicationUser();
            string userPw4 = "123456";
            var userInfo4 = new MyUserInfo() { FirstName = "Wei", LastName = "Chen" };
            UserWei.UserName = "Wei";
            UserWei.HomeTown = "Toronto";
            UserWei.MyUserInfo = userInfo4;
            var UserWeiCreate = UserManager.Create(UserWei, userPw4);
            if (UserWeiCreate.Succeeded) {
                var addUserWeiToRole2Result = UserManager.AddToRole(UserWei.Id, roleName2);
            }

            var UserJohn = new ApplicationUser();
            string userPw5 = "123456";
            var userInfo5 = new MyUserInfo() { FirstName = "John", LastName = "Woo" };
            UserJohn.UserName = "John";
            UserJohn.HomeTown = "Toronto";
            UserJohn.MyUserInfo = userInfo5;
            var UserJohnCreate = UserManager.Create(UserJohn, userPw5);
            if (UserJohnCreate.Succeeded) {
                var addUserJohnToRole2Result = UserManager.AddToRole(UserJohn.Id, roleName2);
            }

            var UserJack = new ApplicationUser();
            string userPw6 = "123456";
            var userInfo6 = new MyUserInfo() { FirstName = "Jack", LastName = "Smith" };
            UserJack.UserName = "Jack";
            UserJack.HomeTown = "Toronto";
            UserJack.MyUserInfo = userInfo6;
            var UserJackCreate = UserManager.Create(UserJack, userPw6);
            if (UserJackCreate.Succeeded) {
                var addUserJackToRole2Result = UserManager.AddToRole(UserJack.Id, roleName2);
            }

            var UserJill = new ApplicationUser();
            string userPw7 = "123456";
            var userInfo7 = new MyUserInfo() { FirstName = "Jill", LastName = "Smith" };
            UserJill.UserName = "Jill";
            UserJill.HomeTown = "Toronto";
            UserJill.MyUserInfo = userInfo7;
            var UserJillCreate = UserManager.Create(UserJill, userPw7);
            if (UserJillCreate.Succeeded) {
                var addUserJillToRole2Result = UserManager.AddToRole(UserJill.Id, roleName2);
            }

            //Faculties - Rolename 3

            var UserPeter = new ApplicationUser();
            string userPw8 = "123456";
            var userInfo8 = new MyUserInfo() { FirstName = "Peter", LastName = "Peterson" };
            UserPeter.UserName = "Peter"; // IdentityUser.UserName (string)
            UserPeter.HomeTown = "Toronto";
            UserPeter.MyUserInfo = userInfo8;
            var UserPeterCreate = UserManager.Create(UserPeter, userPw8);
            if (UserPeterCreate.Succeeded) {
                var addUserPeterToRole3Result = UserManager.AddToRole(UserPeter.Id, roleName3);
            }

            var UserAdam = new ApplicationUser();
            string userPw9 = "123456";
            var userInfo9 = new MyUserInfo() { FirstName = "Adam", LastName = "Adamson" };
            UserAdam.UserName = "Adam";
            UserAdam.HomeTown = "Toronto";
            UserAdam.MyUserInfo = userInfo9;
            var UserAdamCreate = UserManager.Create(UserAdam, userPw9);
            if (UserAdamCreate.Succeeded) {
                var addUserAdamToRole3Result = UserManager.AddToRole(UserAdam.Id, roleName3);
            }

            var UserRon = new ApplicationUser();
            string userPw10 = "123456";
            var userInfo10 = new MyUserInfo() { FirstName = "Ronald", LastName = "Ronaldson" };
            UserRon.UserName = "Ron";
            UserRon.HomeTown = "Toronto";
            UserRon.MyUserInfo = userInfo3;
            var UserRonCreate = UserManager.Create(UserRon, userPw10);
            if (UserRonCreate.Succeeded) {
                var addUserRonToRole3Result = UserManager.AddToRole(UserRon.Id, roleName3);
            }

            var UserBill = new ApplicationUser();
            string userPw11 = "123456";
            var userInfo11 = new MyUserInfo() { FirstName = "Bill", LastName = "Johnson" };
            UserBill.UserName = "Bill";
            UserBill.HomeTown = "Toronto";
            UserBill.MyUserInfo = userInfo11;
            var UserBillCreate = UserManager.Create(UserBill, userPw11);
            if (UserBillCreate.Succeeded) {
                var addUserBillToRole3Result = UserManager.AddToRole(UserBill.Id, roleName3);
            } // if

            // Initialize tables


            try {

                
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
                Student bob = new Student();
                bob.PersonId = 1;
                bob.FirstName = "Bob";
                bob.LastName = "White";
                bob.Phone = "555-555-5555";
                bob.SenecaId = "011111111";
                bob.UserName = "Bob_";
                //bob.UserName = UserBob.UserName; // 33
                dc.Students.Add(bob);
                // mary
                Student mary = new Student();
                mary.PersonId = 2;
                mary.FirstName = "Mary";
                mary.LastName = "Brown";
                mary.Phone = "555-555-5555";
                mary.SenecaId = "011111112";
                mary.UserName = "Mary_";
                //mary.UserName = user3.UserName; // 33
                dc.Students.Add(mary);
                // wei
                Student wei = new Student();
                wei.PersonId = 3;
                wei.FirstName = "Wei";
                wei.LastName = "Chen";
                wei.Phone = "555-555-5555";
                wei.SenecaId = "011111113";
                wei.UserName = "Wei_";
                //wei.UserName = UserWei.UserName; // 33
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

                dc.SaveChanges();

                // create faculty Peter
                Faculty f = new Faculty("Peter", "Peterson", "555-567-6789", "034234678"); // 20
                f.PersonId = 10; // 25
                f.UserName = "Peter_";
                dc.Faculties.Add(f);

                // 1) Peter teaches IPC144
                Course c = new Course();
                c.CourseId = 1;
                c.CourseCode = "IPC144";
                c.CourseName = "Introduction into programming";
                c.RoomNumber = "1000";  
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>(); // 36
                c.Users.Add(UserPeter); // 17
                f.Courses.Add(c); // 34
                c.Students = new List<Student>(); // 37
                c.Students.Add(bob); // 35
                bob.Courses.Add(c);
                c.Users.Add(UserBob);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 2) Peter teaches ULI101
                c = new Course();
                c.CourseId = 2;
                c.CourseCode = "ULI101";
                c.CourseName = "OS - Unix";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserPeter);
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Users.Add(UserMary);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 3) Peter teaches IOS110
                c = new Course();
                c.CourseId = 3;
                c.CourseCode = "IOS110";
                c.CourseName = "OS - Windows";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserPeter);
                c.Students.Add(wei);
                wei.Courses.Add(c);
                c.Users.Add(UserWei);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 4) Peter teaches OOP244
                c = new Course();
                c.CourseId = 4;
                c.CourseCode = "OOP244";
                c.CourseName = "OOP development using C++";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserPeter);
                c.Students.Add(bob);
                bob.Courses.Add(c);
                c.Students.Add(john);
                john.Courses = new List<Course>();
                john.Courses.Add(c);
                c.Users.Add(UserBob);
                c.Users.Add(UserJohn);
                dc.Courses.Add(c);
                c = null;
                f = null;
                dc.SaveChanges();

                // create faculty Adam
                f = new Faculty("Adam", "Adamson", "555-567-6790", "034234677");
                f.PersonId = 11;
                f.UserName = "Adam_";
                dc.Faculties.Add(f); // 35


                // 1) Adam teaches INT222
                c = new Course();
                c.CourseId = 5;
                c.CourseCode = "INT222";
                c.CourseName = "Web development - client";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserAdam);
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses = new List<Course>();
                jack.Courses.Add(c);
                c.Users.Add(UserMary);
                c.Users.Add(UserJack);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 2) Adam teaches IBC233
                c = new Course();
                c.CourseId = 6;
                c.CourseCode = "IBC233";
                c.CourseName = "iSeries - Business Applications";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserAdam);
                c.Students.Add(wei);
                wei.Courses.Add(c);
                c.Students.Add(jill);
                jill.Courses = new List<Course>();
                jill.Courses.Add(c);
                c.Users.Add(UserWei);
                c.Users.Add(UserJill);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 3) Adam teaches DBS201
                c = new Course();
                c.CourseId = 7;
                c.CourseCode = "DBS201";
                c.CourseName = "Database principles";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserAdam);
                c.Students.Add(bob);
                bob.Courses.Add(c);
                c.Students.Add(john);
                john.Courses.Add(c);
                c.Users.Add(UserBob);
                c.Users.Add(UserJohn);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 4) Adam teaches OOP344
                c = new Course();
                c.CourseId = 8;
                c.CourseCode = "OOP344";
                c.CourseName = "OOP development - C++";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserAdam);
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses.Add(c);
                c.Users.Add(UserMary);
                c.Users.Add(UserJack);
                dc.Courses.Add(c);
                c = null;
                f = null;
                dc.SaveChanges();

                // create faulty Ron
                f = new Faculty("Ronald", "Ronaldson", "555-567-6791", "034234676");
                f.PersonId = 12;
                f.UserName = "Ron_";
                dc.Faculties.Add(f); // 35

                // 1) Ron teaches INT322
                c = new Course();
                c.CourseId = 9;
                c.CourseCode = "INT322";
                c.CourseName = "Web development - Unix server";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserRon);
                c.Students.Add(wei);
                wei.Courses.Add(c);
                c.Students.Add(jill);
                jill.Courses.Add(c);
                c.Users.Add(UserWei);
                c.Users.Add(UserJill);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 2) Ron teaches DBS301
                c = new Course();
                c.CourseId = 10;
                c.CourseCode = "DBS301";
                c.CourseName = "Database design and development";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserRon);
                c.Students.Add(jill);
                jill.Courses.Add(c);
                c.Users.Add(UserJill);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 3) Ron teaches JAC444
                c = new Course();
                c.CourseId = 11;
                c.CourseCode = "JAC444";
                c.CourseName = "OOP develoment - Java";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserRon);
                c.Students.Add(bob);
                bob.Courses.Add(c);
                c.Students.Add(john);
                john.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses.Add(c);
                c.Users.Add(UserBob);
                c.Users.Add(UserJohn);
                c.Users.Add(UserJack);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 4) Ron teaches INT422
                c = new Course();
                c.CourseId = 12;
                c.CourseCode = "INT422";
                c.CourseName = "Web development - Wondows";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserRon);
                c.Students.Add(bob);
                bob.Courses.Add(c);
                c.Users.Add(UserBob);
                dc.Courses.Add(c);
                c = null;
                f = null;
                dc.SaveChanges();

                // create faculty Bill
                f = new Faculty("Bill", "Johnson", "555-567-6792", "034234677");
                f.PersonId = 13;
                f.UserName = "Bill_";
                dc.Faculties.Add(f); // 35

                // 1) Bill teaches DCN455
                c = new Course();
                c.CourseId = 13;
                c.CourseCode = "DCN455";
                c.CourseName = "Data communication for developers";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserBill);
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Users.Add(UserMary);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 2) Bill teaches BAC344
                c = new Course();
                c.CourseId = 14;
                c.CourseCode = "BAC344";
                c.CourseName = "Business apps - Cobol";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserBill);
                c.Students.Add(mary);
                mary.Courses.Add(c);
                c.Students.Add(jack);
                jack.Courses.Add(c);
                c.Users.Add(UserMary);
                c.Users.Add(UserJack);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 3) Bill teaches MAP524
                c = new Course();
                c.CourseId = 15;
                c.CourseCode = "MAP524";
                c.CourseName = "Mobile apps - Android";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserBill);
                c.Students.Add(wei);
                wei.Courses.Add(c);
                c.Students.Add(jill);
                jill.Courses.Add(c);
                c.Users.Add(UserWei);
                c.Users.Add(UserJill);
                dc.Courses.Add(c);
                c = null;
                dc.SaveChanges();

                // 4) Bill teaches  WIN210
                c = new Course();
                c.CourseId = 16;
                c.CourseCode = "WIN210";
                c.CourseName = "Windows administration";
                c.RoomNumber = "1000";
                c.RunTime = "1000";
                c.Faculty = f;
                c.Users = new List<ApplicationUser>();
                c.Users.Add(UserBill);
                c.Students.Add(john);
                john.Courses.Add(c);
                c.Users.Add(UserJohn);
                dc.Courses.Add(c);
                //----------------------
                bob.Courses.Count();
                mary.Courses.Count();
                wei.Courses.Count();
                john.Courses.Count();
                jack.Courses.Count();
                jill.Courses.Count();
                //--------------------

                dc.SaveChanges();

                //var course = dc.Courses;

            } // try
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
        } // InitializeIdentityForEF()

        protected override void Seed(DataContext dc) {
            InitializeIdentityForEF(dc);
            base.Seed(dc);
        }


        public List<Student> Students { get; set; }
        public List<Faculty> Faculties { get; set; }
        public List<Course> Courses { get; set; }

    }
}