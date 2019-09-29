using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpdesk.Models
{
    public class FileDetails
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public int TicketsId { get; set; }
        
        public virtual Tickets Tickets { get; set; }
    }
}