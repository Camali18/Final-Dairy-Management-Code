using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DairyManagementSystem.Models
{
    public class GenerateModel
    {
        [Required]
        public int UserId { get; set; }

        public string VendorName { get; set; }
        [Required]
        public double quantity { get; set; }
        [Required]
        public string timing { get; set; }
        [Required]
        public string fat { get; set; }

        public int vend { get; set; }

        public int fatid { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public int Price { get; set; }

        public int TotalMilkAmount { get; set; }

        public int TotalGrossAmount { get; set; }
    }
}