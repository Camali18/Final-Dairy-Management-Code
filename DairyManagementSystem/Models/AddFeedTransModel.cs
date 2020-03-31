using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DairyManagementSystem.Models
{
    public class AddFeedTransModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int VendorId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string ItemName { get; set; }
        public int vend { get; set; }
        public string VendorName { get; set; }
        public int FeedId { get; set; }
        public int ItemId { get; set; }


    }
}