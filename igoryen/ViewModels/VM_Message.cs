using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace igoryen.ViewModels {
  // Classes alphabetically

  //======================================
  // MessageBase
  //======================================
  public class MessageBase {

    [Key]
    public int MessageId { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
  }

  //======================================
  // MessageCreate
  //======================================
  public class MessageCreate {
    [Key]
    public int MessageId { get; set; }
    [Required]
    public string Date { get; set; }
    public string Time { get; set; }
    public string CourseName { get; set; }
    public int FacultyId { get; set; }
    public string Body { get; set; }
    public string CustomMsg { get; set; }
  }


  //======================================
  // MessageFull
  //======================================
  public class MessageFull : MessageBase {
    public string CourseName { get; set; }
    public FacultyFull Faculty { get; set; }
    public string Body { get; set; }
    public string CustomMsg { get; set; }

    public MessageFull() {
      this.Faculty = new FacultyFull();
    }
  }
}