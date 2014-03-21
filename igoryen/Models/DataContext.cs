// 10. to bring in IdentityDbContext<>
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework; // 10
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace igoryen.Models {
  public class DataContext : IdentityDbContext<ApplicationUser> {
    public DataContext() : base("DefaultConnection") { }
    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      // Change the name of the table to be Users instead of AspNetUsers
      modelBuilder.Entity<IdentityUser>().ToTable("Users");
      modelBuilder.Entity<ApplicationUser>().ToTable("Users");
    }

    public DbSet<Cancellation> Cancellations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    //public DbSet<Message> Messages { get; set; }
    public DbSet<MyUserInfo> MyUserInfo { get; set; }
    public DbSet<Student> Students { get; set; }



    public System.Data.Entity.DbSet<igoryen.ViewModels.StudentFull> StudentFulls { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.FacultyPublic> FacultyPublics { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.CourseFull> CourseFulls { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.CourseBase> CourseBases { get; set; }

    public System.Data.Entity.DbSet<igoryen.ViewModels.StudentName> StudentNames { get; set; }
  }
}