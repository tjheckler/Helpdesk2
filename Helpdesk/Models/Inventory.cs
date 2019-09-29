using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpdesk.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string ComputerName { get; set; }
        public string CurrentLocation { get; set; }
        public string CurrentUser { get; set; }
        public string BuildingLocation { get; set; }
    }
}