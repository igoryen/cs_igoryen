// 10. to bring in class IdentityUser
using igoryen.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework; // 10
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace igoryen.Models {
  // classes alphabetically: Cancellation, Course, Faculty, Message, MyUserInfo, MyDbContext, Person, Student

  // A

  //===================================================
  // ApplicationUser - moved to Models/IdentityModels.cs
  //===================================================
  /*
  public class ApplicationUser : IdentityUser {
    // HomeTown will be stored in the same table as Users
    public string HomeTown { get; set; }
    public virtual ICollection<ToDo> ToDoes { get; set; }

    // FirstName & LastName will be stored in a different table called MyUserInfo
    public virtual MyUserInfo MyUserInfo { get; set; }
  }
  */
  // C

  //===================================================
  // Cancellation
  //===================================================
  public class Cancellation {
    public int CancellationId { get; set; }
    //public Faculty Faculty { get; set; }
    public Course Course { get; set; }
    public string Date { get; set; }
    public string Message { get; set; }
    public virtual ApplicationUser User { get; set; }
    //public virtual IdentityUser User { get; set; }
  }

  //===================================================
  // ComMethod - communication method
  //===================================================
  public class ComMethod {
    public int ComMethodId { get; set; }
    public string Handle { get; set; }
    public string CellNo { get; set; }
    public string Email { get; set; }
  }

  //===================================================
  // Course - 7 fields
  // 10. Add ': IdentityDbContext' and you won't see 'Course' 
  //     among class models when you create a view
  //===================================================
  public class Course { // 10
    public Course() {
      this.Students = new List<Student>();
    }
    [Key]
    public int CourseId { get; set; }
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public string RoomNumber { get; set; }
    public string RunTime { get; set; }
    public Faculty Faculty { get; set; }
    public List<Student> Students { get; set; }
    public virtual ApplicationUser User { get; set; }
    //public virtual IdentityUser User { get; set; }

  }

  // F

  //===================================================
  // Faculty
  //===================================================
  public class Faculty : Person {
    public Faculty() {
      this.Courses = new List<Course>();
      SenecaId = string.Empty;
    }

    public Faculty(string fname, string lname, string phone, string senId)
      : base(fname, lname, phone) {
      this.Courses = new List<Course>();
      this.Messages = new List<Message>();
      SenecaId = senId;
    }

    [Required]
    [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
    public string SenecaId { get; set; }
    public List<Course> Courses { get; set; }
    public List<Message> Messages { get; set; }
  }

  // M

  //===================================================
  // Message
  //===================================================
  public class Message {
    public int MessageId { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string CourseName { get; set; }
    public Faculty Faculty { get; set; }
    public string Body { get; set; }
    public string CustomMsg { get; set; }

  }


  //===================================================
  // MyUserInfo
  //===================================================
  public class MyUserInfo {
      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
  }


  //===================================================
  // MyDbContext -- the code should have been in Models/DataContext.cs
  //===================================================
  /*
  public class MyDbContext : IdentityDbContext<ApplicationUser> {
    public MyDbContext()
      : base("DefaultConnection") {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

      // Change the name of the table to be Users instead of AspNetUsers
      modelBuilder.Entity<IdentityUser>().ToTable("Users");
      modelBuilder.Entity<ApplicationUser>().ToTable("Users");
    }
    /*
    public DbSet<Cancellation> Cancellations { get; set; }

    public DbSet<MyUserInfo> MyUserInfo { get; set; }
  
  }*/


  // P

  //===================================================
  // Person
  // 10. Without this line you get the error:
  //     The type 'igoryen.Models.Person' cannot be used as type parameter 'TUser' 
  //     in the generic type or method 'Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext<TUser>'. 
  //     There is no implicit reference conversion 
  //     from 'igoryen.Models.Person' to 'Microsoft.AspNet.Identity.EntityFramework.IdentityUser'.
  //     C:\aaa\vat\c#\proj\igoryen\igoryen\Models\IdentityModels.cs. Line: 27, column: 16

  //===================================================
  public class Person : ApplicationUser { // 10

  //public class Person : IdentityUser { // 10


    public Person() {
      FirstName = LastName = Phone = string.Empty;
    }

    public Person(string f, string l, string p) {
      FirstName = f;
      LastName = l;
      Phone = p;
    }

    [Key]
    public int PersonId { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 3)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required]
    [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
    public string Phone { get; set; }
  }

  // S

  //===================================================
  // Student
  //===================================================
  public class Student : Person {
    public Student() {
      SenecaId = string.Empty;
      this.Courses = new List<Course>();
    }

    public Student(string f, string l, string p, string senId)
      : base(f, l, p) {
        SenecaId = senId;
    }

    [Required]
    [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
    public string SenecaId { get; set; }
    public List<Course> Courses { get; set; }
    //public List<ComMethod> ComMethods { get; set; }
  } // Student
}