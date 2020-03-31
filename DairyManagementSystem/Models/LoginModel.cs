using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DairyManagementSystem.Models
{
    public class LoginModel
    {

        public int Uid { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
        public string Role { get; set; }
        
        public string Address { get; set; }
    }
}