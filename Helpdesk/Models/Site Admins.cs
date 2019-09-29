using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpdesk.Models
{
    public class SiteAdmins
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public long Telephone { get; set; }
        public string Role { get; set; }
    }
}