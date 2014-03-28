using igoryen.ViewModels;
using igoryen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace igoryen.ViewModels {

  // Classes alphabetically

  // F

  //======================================
  // FacultyBase
  //======================================
  public class FacultyBase {
    [Key]
    public int PersonId { get; set; }

    [Required]
    [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
    public string SenecaId { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 3)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

  }

  //======================================
  // FacultyCreate
  //======================================
  public class FacultyCreate {
    [Key]
    public int FacultyId { get; set; }
    public int SenecaId { get; set; }
    [Required]
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
  }

  //======================================
  // FacultyFull
  //======================================
  public class FacultyFull : FacultyBase {

    [Required]
    [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
    public string Phone { get; set; }
    public List<CourseBase> Courses { get; set; }
    public List<Message> Messages { get; set; }
  }

  //======================================
  // FacultyName
  //======================================
  public class FacultyName {

    [Key]
    public int FacultyId { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 3)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
  }

  //======================================
  // FacultyPublic
  //======================================
  public class FacultyPublic {

    [Key]
    public int FacultyId { get; set; }

    [Required]
    [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
    public string SenecaId { get; set; }

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
}