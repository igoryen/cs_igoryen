using igoryen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace igoryen.ViewModels {
  
  // Classes alphabetically

  //===============================
  // StudentBase
  //===============================
  public class StudentBase {
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

  //===============================
  // StudentFull : StudentBase
  //===============================
  public class StudentFull : StudentBase {

    public StudentFull() {
      this.Courses = new List<CourseBase>();
    }

    [Required]
    [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
    public string Phone { get; set; }

    public List<CourseBase> Courses { get; set; }
    public List<ComMethod> ComMethods { get; set; }
  }
  
  //===============================
  // StudentName
  //===============================
  public class StudentName {

    [Key]
    public int StudentId { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 3)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
  }

  //===============================
  // StudentPublic
  //===============================
  public class StudentPublic {

    [Key]
    public int StudentId { get; set; }

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