using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Job_Portal_MVC.Models
{
    public class User
    {
        [Key]
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password should be minimum of 8 characters", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and should contain : upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password required")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Display(Name = "ContactNumber")]
        [Required(ErrorMessage = "Contact number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Your mobile number  is not valid")]
        [RegularExpression(@"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$", ErrorMessage = "Please enter a valid mobile number")]
        public string ContactNumber { get; set; }
        [Required]
        [Display(Name ="Highest Qualification")]
        public string Qualification { get; set; }
        [Required]
        [Display(Name ="Year of Passout")]
        public string year { get; set; }
        [Required]
        [Display(Name = "Expiernced/Fresher")]
        public string Experience;
        [Required]
        [Display(Name ="Last Employer")]
        public string Employer { get; set; }
        [Required]
        [Display(Name = "Employee Details")]
        public string EmployerDetails { get; set; }


    }
    public enum Qualification
    {
         No_formal_education,
        Primary_education,
        Secondary_education_or_high_school,
        GED,
        Vocational_qualification,
         Bachelor_Degree,
         Master_Degree,
         Doctorate_or_higher
    }
    public enum Experiences
    {
        Experienced,
        Fresher

    }

}