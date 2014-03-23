using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
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
    public virtual MyUserInfo MyUserInfo { get; set; } // 30
  }

  //===================================================
  // ApplicationDbContext
  //===================================================
  public class DataContext : IdentityDbContext<ApplicationUser> {
    public DataContext()
      : base("DefaultConnection") {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      // Change the name of the table to be Users instead of AspNetUsers
      modelBuilder.Entity<IdentityUser>().ToTable("Users");
      modelBuilder.Entity<ApplicationUser>().ToTable("Users");
    }

    public DbSet<Cancellation> Cancellations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<MyUserInfo> MyUserInfo { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Message> Messages { get; set; }



    public System.Data.Entity.DbSet<igoryen.ViewModels.StudentFull> StudentFulls { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.FacultyPublic> FacultyPublics { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.CourseFull> CourseFulls { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.CourseBase> CourseBases { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.StudentName> StudentNames { get; set; }
    public System.Data.Entity.DbSet<igoryen.ViewModels.MessageFull> MessageFulls { get; set; }

  }
}