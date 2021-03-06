using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser, AppUserRole, string>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<AssessmentType> AssessmentType { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestion { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne<Position>(p => p.Position)
                .WithMany(a => a.AppUser)
                .HasForeignKey(p => p.PositionId);

            builder.Entity<AppUser>()
                .HasOne<Department>(d => d.Department)
                .WithMany(a => a.AppUser)
                .HasForeignKey(d => d.DepartmentId);

            builder.Entity<AppUser>()
                .HasOne<Company>(c => c.Company)
                .WithMany(a => a.AppUser)
                .HasForeignKey(c => c.CompanyId);

            builder.Entity<AssessmentQuestion>()
                .HasOne<AssessmentType>(at => at.AssessmentType)
                .WithMany(aq => aq.AssessmentQuestion)
                .HasForeignKey(at => at.AssessmentTypeId);
        }
    }
}