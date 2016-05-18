using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaprisaProject.Models
{
    public class userdetials
    {
        [Required]

        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class RoleNames
    {
        [Required]
        [Display(Name = "ID")]
        public string id { get; set; }

        [Required]

        [Display(Name = "Roles")]
        public string Role { get; set; }
    }
    public class UserRole
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "User")]
        public string name { get; set; }



        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}