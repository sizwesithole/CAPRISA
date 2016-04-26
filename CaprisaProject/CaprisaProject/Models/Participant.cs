using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CaprisaProject.Models
{
    public enum Site
    {
        Vulindlela,
        eThekwini,
        Springfield
    }
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name ="Participant ID")]
        [DataType(DataType.Text,ErrorMessage ="A participant code is required.")]
        public string ParticipantCode { get; set; }

        [Required]
        [Display(Name ="Participant Enrollment Date")]
        [DataType(DataType.Date,ErrorMessage ="Please ensure the participant enrollment date is correct and in the correct format.")]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        [Display(Name ="Participant Phone Number")]
        [DataType(DataType.Text,ErrorMessage ="Please Enter Phone Number starting with +27")]
        [MinLength(10),MaxLength(12)]
        public string PhoneNumber { get; set; }
        

        [Required]
        [Display(Name ="Residence Site")]
        public Site Site { get; set; }

        public virtual ICollection <Enrollment> Enrollments { get; set; }
        public virtual ICollection <Study> Studies { get; set; } 
    }
}