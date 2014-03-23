using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace igoryen.ViewModels {
  // Classes alphabetically

  //======================================
  // CourseBase
  //======================================
  public class CourseBase {

    [Key]
    public int CourseId { get; set; }
    public string CourseCode { get; set; }
  }

  //======================================
  // CourseCreate
  //======================================
  public class CourseCreate {
    [Key]
    public int Id { get; set; }
    [Required]
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public int FacultyId { get; set; }
  }


  //======================================
  // CourseFull
  //======================================
  public class CourseFull : CourseBase {
    public string CourseName { get; set; }
    public FacultyFull Faculty { get; set; }
    public List<StudentBase> Students { get; set; }

    public CourseFull() {
      this.Faculty = new FacultyFull();
      this.Students = new List<StudentBase>();
    }
  }
}