using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Helpdesk.Models;
using System.Data.Entity;
using System.Web.Mvc;
namespace Helpdesk.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}