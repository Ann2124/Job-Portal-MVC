﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_MVC.Models
{
    public class Application
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
    public enum Experience
    {
        No_Experience,
        Upto_6_Months,
        One_Year,
        One_year_to_Two_years,
        More_than_two_years
    }
}