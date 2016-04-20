using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CaprisaProject.Models;
namespace CaprisaProject.DAL
{
    public class CaprisaContext : DbContext
    {
        public CaprisaContext()
            :base("DefaultConnection")
        {

        }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Study> Studies { get; set;}
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<CaprisaProject.Models.CohortCoordinator> CohortCoordinators { get; set; }

        public System.Data.Entity.DbSet<CaprisaProject.Models.RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<CaprisaProject.Models.EditUserViewModel> EditUserViewModels { get; set; }
        public System.Data.Entity.DbSet<CaprisaProject.Models.Appointment> Appointments { get; set; }
    }
}