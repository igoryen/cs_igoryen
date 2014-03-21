using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;


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
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
    public ApplicationDbContext()
      : base("DefaultConnection") {
    }
  }
}