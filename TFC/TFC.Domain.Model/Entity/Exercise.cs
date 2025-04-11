using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TFC.Domain.Model.Entity
{
    public class Exercise
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? ExerciseName { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }
    }
}