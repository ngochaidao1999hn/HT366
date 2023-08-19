using HT366.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HT366.Infrastructure.Persistence
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>()
            .HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Exam>()
            .HasQueryFilter(c => !c.IsDeleted)
            .HasOne(c => c.User)
            .WithMany(u => u.Exams)
            .HasForeignKey(c => c.CreatedBy);
            builder.Entity<Exercise>()
            .HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Lesson>()
            .HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Domain.Entities.File>()
            .HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
