using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_MVC.Models
{
    public class Employer
    {
        [Key]
        [Required]
        [Display(Name = "Employer ID")]
        public string employerId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password should be minimum of 8 characters", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and should contain : upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string password { get; set; }
        
        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "mobile number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Your mobile number  is not valid")]
        [RegularExpression(@"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$", ErrorMessage = "Please enter a valid mobile number")]
        public string mobileNo { get; set; }
        [Required]
        public string Organisation { get; set; }

    
        
    }
}
    