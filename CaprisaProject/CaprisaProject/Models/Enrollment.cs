using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CaprisaProject.Models
{
    
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Enrollment ID")]
        public int EnrollmentID { get; set; }

        [Required]
        [Display(Name ="Study Code")]
        [DataType(DataType.Text)]
        public string StudyCode { get; set; }
        
        [Required]
        [Display(Name ="Particpant Code")]
        [DataType(DataType.Text)]
        public string ParticipantCode { get; set; }
        
        public Site Site { get; set; }

        public virtual Study Study { get; set; }
        public virtual Participant Participant { get; set; }
    }
}