using Microsoft.EntityFrameworkCore;
using TFC.Domain.Model.Entity;
using TFC.Domain.Model.Enum;

namespace TFC.Infraestructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<SplitDay> SplitDays { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.UserId).HasName("pk_users");

                entity.Property(u => u.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedOnAdd();

                entity.Property(u => u.Dni)
                    .HasColumnName("dni")
                    .HasMaxLength(9);

                entity.Property(u => u.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);

                entity.Property(u => u.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.FriendCode)
                    .HasColumnName("friend_code");

                entity.Property(u => u.Password)
                    .HasColumnName("password");

                entity.Property(u => u.Role)
                    .HasColumnName("role")
                    .HasConversion<string>();

                entity.Property(u => u.InscriptionDate)
                    .HasColumnName("inscription_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(u => u.Dni)
                    .IsUnique()
                    .HasDatabaseName("ix_users_dni");

                entity.HasIndex(u => u.Email)
                    .IsUnique()
                    .HasDatabaseName("ix_users_email");

                entity.HasMany(u => u.Routines)
                    .WithOne(r => r.User)
                    .HasForeignKey(r => r.UserId)
                    .HasConstraintName("fk_routines_user")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para Routine
            modelBuilder.Entity<Routine>(entity =>
            {
                entity.ToTable("routines");
                entity.HasKey(r => r.RoutineId).HasName("pk_routines");

                entity.Property(r => r.RoutineId)
                    .HasColumnName("routine_id")
                    .ValueGeneratedOnAdd();

                entity.Property(r => r.UserId)
                    .HasColumnName("user_id");

                entity.Property(r => r.RoutineName)
                    .HasColumnName("routine_name")
                    .HasMaxLength(100);

                entity.Property(r => r.RoutineDescription)
                    .HasColumnName("routine_description")
                    .HasMaxLength(500);

                entity.HasMany(r => r.SplitDays)
                    .WithOne(sd => sd.Routine)
                    .HasForeignKey(sd => sd.RoutineId)
                    .HasConstraintName("fk_split_days_routine")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para SplitDay con clave primaria compuesta y enum guardado como string
            modelBuilder.Entity<SplitDay>(entity =>
            {
                entity.ToTable("split_days");

                entity.HasKey(sd => new { sd.RoutineId, sd.DayName })
                      .HasName("pk_split_days");

                entity.Property(sd => sd.RoutineId)
                    .HasColumnName("routine_id");

                entity.Property(sd => sd.DayName)
                    .HasColumnName("day_name")
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.Property(sd => sd.DayExercisesDescription)
                    .HasColumnName("day_exercises_description")
                    .IsRequired();

                entity.HasOne(sd => sd.Routine)
                    .WithMany(r => r.SplitDays)
                    .HasForeignKey(sd => sd.RoutineId)
                    .HasConstraintName("fk_split_days_routine")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(sd => sd.Exercises)
                    .WithOne(e => e.SplitDay)
                    .HasForeignKey(e => new { e.RoutineId, e.DayName })
                    .HasConstraintName("fk_exercises_split_day")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para Exercise
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("exercises");
                entity.HasKey(e => e.ExerciseId).HasName("pk_exercises");

                entity.Property(e => e.ExerciseId)
                    .HasColumnName("exercise_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RoutineId)
                    .HasColumnName("routine_id");

                entity.Property(e => e.DayName)
                    .HasColumnName("day_name")
                    .HasConversion<string>()
                    .HasMaxLength(20);

                entity.Property(e => e.ExerciseName)
                    .HasColumnName("exercise_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Sets)
                    .HasColumnName("sets");

                entity.Property(e => e.Reps)
                    .HasColumnName("reps");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight");

                entity.HasOne(e => e.Routine)
                    .WithMany()
                    .HasForeignKey(e => e.RoutineId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configuración para UserFriend
            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.ToTable("user_friends");
                entity.HasKey(uf => uf.Id).HasName("pk_user_friends");

                entity.Property(uf => uf.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(uf => uf.UserId)
                    .HasColumnName("user_id");

                entity.Property(uf => uf.FriendId)
                    .HasColumnName("friend_id");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(uf => uf.UserId)
                    .HasConstraintName("fk_user_friend_user")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(uf => uf.FriendId)
                    .HasConstraintName("fk_user_friend_friend")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        // Ya no es necesario configurar convenciones para WeekDay
    }
}
