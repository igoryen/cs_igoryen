using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        public FacultyBase Faculty { get; set; }
        [Required]
        public string CourseCode { get; set; }
        public string Date { get; set; }
    }

    //======================================
    // CancellationCreateForHttpGet
    //======================================
    public class CancellationCreateForHttpGet {
        [Key]
        public int CancellationId { get; set; }
        public string Message { get; set; }

        [Display(Name = "Date :)")]
        public string Date { get; set; }

        public SelectList CourseSelectList { get; set; }

        public void Clear() {
            Message = string.Empty;
            Date = string.Empty;
        }
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