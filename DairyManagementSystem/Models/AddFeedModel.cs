using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DairyManagementSystem.Models
{
    public class AddFeedModel
    {
            [Required]
            public string Name { get; set; }
            [Required]
            public int Price { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public int Stock { get; set; }
        }

    }
