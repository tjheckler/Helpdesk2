using Helpdesk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Helpdesk.Models
{
    public class HelpdeskDb : DbContext
    {
        //public HelpdeskDb()
                   // :base("name=DefaultConnection"){}
                   
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SiteAdmins> Site_Admins { get; set; }
        public DbSet<Priorities> Priorities { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
    }
}