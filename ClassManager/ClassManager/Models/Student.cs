using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ClassManager.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "The name cannot be blank")]
        [StringLength(50,MinimumLength =3, ErrorMessage="Please enter a name between 3 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a name beginning with a letter and enter only letters and spaces.")]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Student ID cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a Student ID between 3 and 50 digits in length")]
        [RegularExpression(@"^[0-9]+[0-9]*$", ErrorMessage = "Please enter a Student ID only digits.")]
        [Display(Name = "Student ID")]
        public string StudentNumber { get; set; }

        [Required(ErrorMessage = "The Enrollment Year cannot be blank")]
        [Display(Name = "Enrollment Year")]
        public int EnrollmentYear { get; set; }

        [Required(ErrorMessage = "The Gender cannot be blank")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The Age cannot be blank")]
        public int Age { get; set; }
        public string TeacherId { get; set; }
    }
}