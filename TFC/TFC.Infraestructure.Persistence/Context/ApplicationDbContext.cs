using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TFC.Domain.Model.Entity;

namespace TFC.Infraestructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {  }

        public DbSet<User> Users { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<SplitDay> SplitDays { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Dni).IsRequired().HasMaxLength(20);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);

                entity.HasIndex(u => u.Dni).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Routine>(entity =>
            {
                entity.HasKey(r => r.RoutineId);
                entity.Property(r => r.RoutineName).IsRequired().HasMaxLength(100);
                entity.Property(r => r.RoutineDescription).HasMaxLength(500);

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Routines)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SplitDay>(entity =>
            {
                entity.HasKey(sd => new { sd.RoutineId, sd.DayName });

                entity.HasOne(sd => sd.Routine)
                      .WithMany(r => r.SplitDays)
                      .HasForeignKey(sd => sd.RoutineId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.ExerciseId);
                entity.Property(e => e.ExerciseName).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.SplitDay)
                      .WithMany(sd => sd.Exercises)
                      .HasForeignKey(e => new { e.RoutineId, e.DayName })
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}