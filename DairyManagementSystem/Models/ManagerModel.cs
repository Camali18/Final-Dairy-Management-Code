using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DairyManagementSystem.Models
{
    public class ManagerModel
    {
        [Required]
        public int UserId { get; set; }

        public string VendorName { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public string timing { get; set; }
        [Required]
        public int fat { get; set; }
        [Required]
        public DateTime date { get; set; }
    }
    
}