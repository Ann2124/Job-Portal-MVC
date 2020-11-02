using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Job_Portal_MVC.Models
{
    public class JobPortalContext:DbContext
    {
        public JobPortalContext() : base("Name=Dbconfig")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Openings> Openings { get; set; }
        public DbSet<Employer> Employerss { get; set; }
        public DbSet<application> Applications { get; set; }
      

    }
}