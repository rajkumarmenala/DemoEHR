using Monad.EHR.Domain.Entities;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Monad.EHR.Domain.Entities.Identity;

namespace Monad.EHR.Infrastructure.Data
{
    public class CustomDBContext : IdentityDbContext<User, Role, string>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           			modelBuilder.Entity<Patient>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("PatientID");
            });
			modelBuilder.Entity<Address>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("AddressID");
            });
			modelBuilder.Entity<Medications>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("MedicationsID");
            });
			modelBuilder.Entity<Problems>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("ProblemsID");
            });
			modelBuilder.Entity<BP>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("BPID");
            });
			modelBuilder.Entity<PatientHeight>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("PatientHeightID");
            });
			modelBuilder.Entity<Weight>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("WeightID");
            });

          
           
            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("ClaimID");
            });

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("User");
                b.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Activity>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("ActivityID");
            });

            modelBuilder.Entity<ActivityRole>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("ActivityRoleID");
            });

			modelBuilder.Entity<Resource>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("ResourceID");
            });
			modelBuilder.Entity<ResourceType>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("ResourceTypeID");
            });
            modelBuilder.Entity<RoleRight>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("RoleRightID");
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
