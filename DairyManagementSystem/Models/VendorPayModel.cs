using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyManagementSystem.Models
{
    public class VendorPayModel
    {
        public int PayId { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public double GrossAmount { get; set; }
        public double FeedAmount { get; set; }
        public double MilkAmount { get; set; }
        public int VendorId { get; set; }
        public int UserId { get; set; }
        public string VendorName { get; set; }
    }
}