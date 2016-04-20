using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaprisaProject.Models;
using System.Data.Entity;

namespace CaprisaProject.DAL
{
    public class CaprisaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CaprisaContext>

    {
        protected override void Seed(CaprisaContext context)
        {
            var participants = new List<Participant>
            {
                new Participant {ParticipantCode="cap1234", EnrollmentDate=DateTime.Parse("2002-09-01"),PhoneNumber="+27748458785",  Site=Site.Vulindlela  },
                new Participant {ParticipantCode="cap4321", EnrollmentDate=DateTime.Parse("2012-07-02"),PhoneNumber="+27744308900",  Site=Site.eThekwini  },
                new Participant {ParticipantCode="cap2546", EnrollmentDate=DateTime.Parse("2007-11-04"),PhoneNumber="+27744569874",  Site=Site.Springfield   }
            };

            participants.ForEach(p => context.Participants.Add(p));
            context.SaveChanges();
            var studies = new List<Study>
            {
                new Study { StudyCode="MDR-007", Title="MDR Study"},
                new Study { StudyCode="HIV-017", Title="HIV Study"},
                new Study { StudyCode="TB-027", Title="TB Study"}
            };
            studies.ForEach(p => context.Studies.Add(p));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment { ParticipantCode="cap1234", StudyCode="MDR-007", Site=Site.Vulindlela },
                new Enrollment { ParticipantCode="cap4321", StudyCode="HIV-017", Site=Site.eThekwini },
                new Enrollment { ParticipantCode="cap2546", StudyCode="TB-027", Site=Site.Springfield },
            };
            enrollments.ForEach(p => context.Enrollments.Add(p));
            context.SaveChanges();

            
        }
    }
}