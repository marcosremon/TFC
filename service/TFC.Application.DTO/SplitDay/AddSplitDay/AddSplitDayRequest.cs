﻿using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.SplitDay.AnyadirSplitDay
{
    public class AddSplitDayRequest
    {
        public WeekDay? DayName { get; set; }
        public long? RoutineId { get; set; }
        public long? UserId { get; set; }
        public ICollection<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
    }
}