using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Models;

namespace LearnerCenter.API.Data
{
    public class LearnerCenterDbContext : DbContext
    {
        public LearnerCenterDbContext(DbContextOptions<LearnerCenterDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => 
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        // DbSets for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed basic data (campuses, terms, courses - NO USERS)
            Seeders.BasicDataSeeder.SeedBasicData(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.HasOne(e => e.CurrentEnrollment)
                      .WithMany()
                      .HasForeignKey(e => e.EnrollmentId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure UserProfile entity
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserProfileId);
                entity.HasOne(e => e.User)
                      .WithOne(u => u.UserProfile)
                      .HasForeignKey<UserProfile>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Campus entity
            modelBuilder.Entity<Campus>(entity =>
            {
                entity.HasKey(e => e.CampusId);
                entity.HasIndex(e => e.CampusCode).IsUnique();
                entity.Property(e => e.CampusName).IsRequired();
            });

            // Configure Course entity
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);
                entity.HasIndex(e => new { e.CourseCode, e.EnrollmentId }).IsUnique();
                entity.Property(e => e.CourseCode).IsRequired();
                entity.Property(e => e.CourseName).IsRequired();
                entity.HasOne(e => e.Enrollment)
                      .WithMany(en => en.Courses)
                      .HasForeignKey(e => e.EnrollmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Enrollment entity (Academic Programs)
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId);
                entity.HasIndex(e => new { e.ProgramName, e.CampusId }).IsUnique();
                entity.Property(e => e.ProgramName).IsRequired();
                entity.HasOne(e => e.Campus)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CampusId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Term entity
            modelBuilder.Entity<Term>(entity =>
            {
                entity.HasKey(e => e.TermId);
                entity.HasIndex(e => e.TermCode).IsUnique();
                entity.Property(e => e.TermName).IsRequired();
                entity.Property(e => e.TermCode).IsRequired();
            });



            // Add some constraints and default values with provider-specific SQL
            var utcNowSql = Database.IsNpgsql() ? "NOW() AT TIME ZONE 'UTC'" : "GETUTCDATE()";

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql(utcNowSql);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql(utcNowSql);

            modelBuilder.Entity<Campus>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql(utcNowSql);

            modelBuilder.Entity<Course>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql(utcNowSql);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql(utcNowSql);

            modelBuilder.Entity<Term>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql(utcNowSql);


        }
    }
}