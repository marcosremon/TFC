using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class SplitDay
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public WeekDay DayName { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}