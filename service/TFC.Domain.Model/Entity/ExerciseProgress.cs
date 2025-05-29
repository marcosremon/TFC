using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("exercise_progress")]
public class ExerciseProgress
{
    [Key]
    [Column("progress_id")]
    public long ProgressId { get; set; }

    [Column("exercise_id")]
    public long ExerciseId { get; set; }

    [Column("routine_id")]
    public long RoutineId { get; set; }

    [Column("day_name")]
    public string DayName { get; set; } = string.Empty;

    [Column("sets")]
    public int? Sets { get; set; }

    [Column("reps")]
    public int? Reps { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("performed_at")]
    public DateTime PerformedAt { get; set; }

    // Relaciones (opcional)
    // public Exercise Exercise { get; set; }
}