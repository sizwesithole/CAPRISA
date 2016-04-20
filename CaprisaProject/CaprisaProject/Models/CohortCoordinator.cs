using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CaprisaProject.Models
{
    public class CohortCoordinator
    {
        [Key]
        public int CohortId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please provide cohort Username", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [StringLength(13,ErrorMessage ="Please enter by Caprisa Rule of Cohort Coordinator Username")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public Site Site { get; set; }

        public virtual ICollection<Study> Studies { get; set; }

    }
}