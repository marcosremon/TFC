﻿namespace TFC.Application.DTO.Exercise.DeleteExecise
{
    public class DeleteExerciseRequest
    {
        public string UserEmail { get; set; } = string.Empty;
        public int? RoutineId { get; set; }
        public string? DayName { get; set; }
        public int ExerciseId { get; set; }
    }
}