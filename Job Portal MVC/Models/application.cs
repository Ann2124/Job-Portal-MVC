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
        public int applicationId { get; set; }
        [Required]
        [Display(Name = "Email Id")]
        public string email { get; set; }
        [Required]
        [Display(Name = "Job Id")]
        public int jobId{ get; set; }
        public string status { get; set; }


        public virtual User user { get; set; }
        public virtual Openings Openings { get; set; }

    }
}