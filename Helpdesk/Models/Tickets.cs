using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using System.Collections;
using Helpdesk.Models;
using System.Data;
using static System.Net.WebRequestMethods;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;


namespace Helpdesk.Models
{
    public class Tickets
    {
        public string Priorities { get; set; }
        public string Categories { get; set; }
        public string Site_Admins { get; set; }
        public int TicketsId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Telephone { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
        public string ComputerName { get; set; }
        public long AssetTag { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        //Database Fields For Files

        public virtual ICollection<FileDetails> FileDetails { get; set; }
    }
}
