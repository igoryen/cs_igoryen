using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace igoryen.Models {
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  //===================================================
  // ApplicationUser
  // 10. HomeTown will be stored in the same table as Users
  // 30. FirstName & LastName will be stored in a different table called MyUserInfo
  //===================================================
  public class ApplicationUser : IdentityUser {
    public string HomeTown { get; set; } // 10
    public virtual ICollection<Cancellation> Cancellations { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
    public virtual MyUserInfo MyUserInfo { get; set; } // 30
  }

  //===================================================
  // ApplicationDbContext
  // Person is the base class for Student and Faculty
  //===================================================
  public class DataContext : IdentityDbContext<Person> { // 10
    public DataContext()
      : base("DefaultConnection") {
    }
    //===================================================
    // 30. The entity types 'IdentityUser' and 'Course'/'Person'
    //     cannot share table 'Users' 
    //     because they are not in the same type hierarchy 
    //     or do not have a valid one to one foreign key relationship 
    //     with matching primary keys between them.
    //===================================================
    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      // Change the name of the table to be Users instead of AspNetUsers
      modelBuilder.Entity<IdentityUser>().ToTable("Users");
      modelBuilder.Entity<ApplicationUser>().ToTable("Users");
      //modelBuilder.Entity<Person>().ToTable("Users"); // 30
      //modelBuilder.Entity<Course>().ToTable("Users"); // 30
    }

    public DbSet<Cancellation> Cancellations { get; set; }
    public DbSet<ComMethod> ComMethods { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MyUserInfo> MyUserInfo { get; set; }
    public DbSet<Student> Students { get; set; }



    //public System.Data.Entity.DbSet<igoryen.ViewModels.CancellationFull> CancellationFulls { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.CourseBase> CourseBases { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.CourseFull> CourseFulls { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.FacultyPublic> FacultyPublics { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.StudentFull> StudentFulls { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.StudentName> StudentNames { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.MessageCreate> MessageCreates { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.MessageFull> MessageFulls { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.CancellationCreate> CancellationCreates { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.CourseCreate> CourseCreates { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.StudentBase> StudentBases { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.FacultyBase> FacultyBases { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.FacultyFull> FacultyFulls { get; set; }


  }
}