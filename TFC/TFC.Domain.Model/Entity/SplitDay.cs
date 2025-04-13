using System.ComponentModel.DataAnnotations;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class SplitDay
    {
        [Key]
        public long SplitDayId { get; set; }
        public WeekDay DayName { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}