using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_MVC.Models
{
    public class application
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int application_id { get; set; }
        [Required(ErrorMessage ="Email  Id required")]
        [Display(Name = "Email Id")]
        public string employerId { get; set; }
        [Required(ErrorMessage = " Job Id required")]
        [Display(Name = "Job Id")]
        public int job_id{ get; set; }
        public string status { get; set; }

    }
}