using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace igoryen.ViewModels {

  // Classes alphabetically

  //======================================
  // CancellationBase
  //======================================
  public class CancellationBase {

    [Key]
    public int CancellationId { get; set; }
    public string Date { get; set; }
  }

  //======================================
  // CancellationCreate - 4 fields/forms
  //======================================
  public class CancellationCreate {
    [Key]
    public int CancellationId { get; set; }
    public int FacultyId { get; set; }
    [Required]
    public string CourseCode { get; set; }
    public string Date { get; set; }
  }


  //======================================
  // CancellationFull
  //======================================
  public class CancellationFull : CancellationBase {
    public string CourseName { get; set; }
    public FacultyFull Faculty { get; set; }
    public CourseBase Course { get; set; }

    public CancellationFull() {
      this.Faculty = new FacultyFull();
      this.Course = new CourseBase();
    }
  }
}