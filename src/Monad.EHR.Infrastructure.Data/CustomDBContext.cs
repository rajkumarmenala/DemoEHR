using Monad.EHR.Domain.Entities;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Monad.EHR.Domain.Entities.Identity;
namespace Monad.EHR.Infrastructure.Data
{
    public class CustomDBContext : IdentityDbContext<User>
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

          
            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("RoleID");
            });
            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("ClaimID");
            });
            //modelBuilder.Entity<IdentityUser>(b =>
            //{
            //    b.Table("User");
            //    b.Property(u => u.Id).HasColumnName("UserID");
            //    b.HasKey(u => u.Id);
            //});
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToSqlServerTable("User");
                b.HasKey(u => u.Id);
            });
            base.OnModelCreating(modelBuilder);
            // SetupIdentityDB(modelBuilder);
           
        }
        private void SetupIdentityDB(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
               // b.Collection(u => u.Claims).InverseReference().ForeignKey(uc => uc.UserId);
               // b.Collection(u => u.Logins).InverseReference().ForeignKey(ul => ul.UserId);
               // b.Collection(u => u.Roles).InverseReference().ForeignKey(ur => ur.UserId);
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasColumnName("RoleID");
                b.Property(u => u.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasKey(uc => uc.ClaimID);
            });

            //modelBuilder.Entity<IdentityRoleClaim<TKey>>(b =>
            //{
            //    b.HasKey(rc => rc.Id);
            //    b.ToTable("AspNetRoleClaims");
            //});

            //modelBuilder.Entity<IdeDBsetntityUserRole<TKey>>(b =>
            //{
            //    b.HasKey(r => new { r.UserId, r.RoleId });
            //    b.ToTable("AspNetUserRoles");
            //});
            

            //modelBuilder.Entity<IdentityUserLogin<TKey>>(b =>
            //{
            //    b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //    b.ToTable("AspNetUserLogins");
            //});
        }

    }
}
