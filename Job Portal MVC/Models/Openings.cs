using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Job_Portal_MVC
{
    public class Openings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public double Salary { get; set; }
        [Required]
        [Display(Name = "Experience Required")]
        public string Experience { get; set; }
        [Required]
        [Display(Name = "Qualification Required")]
        public string Qualification { get; set; }
        [Required]
        [Display(Name = "Job Location")]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Vacancies")]
        public int Vacancy { get; set; }
        [Required]
        [Display(Name = "Email")]

        public string Email { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }
    }
}
