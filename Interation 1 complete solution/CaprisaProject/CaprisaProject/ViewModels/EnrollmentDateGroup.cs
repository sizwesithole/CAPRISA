using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CaprisaProject.ViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        [Display(Name ="Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name ="Participant Count")]
        public int ParticipantCount { get; set; }
    }
}