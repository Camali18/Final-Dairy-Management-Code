using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyManagementSystem.Models
{
    public class SupplierPayModel
    {
        public int SupplierId { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public Double Amount { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int UserId { get; set; }

    }
}