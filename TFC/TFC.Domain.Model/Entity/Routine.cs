using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TFC.Domain.Model.Entity
{
    public class Routine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? RoutineName { get; set; }
        public string? RoutineDescription { get; set; }
        public List<SplitDay> SplitDays { get; set; } = new List<SplitDay>();
    }
}