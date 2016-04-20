using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Owin.Security;
using System.Web.Mvc;

namespace CaprisaProject.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }


        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "IT Identification")]
        [StringLength(11, ErrorMessage = "Please use IT Identification Rules, No unAuthorized Registration, Contact System Admin")]
        public string ITid { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }



        public IEnumerable<SelectListItem> RolesList { get; set; }

    }
}