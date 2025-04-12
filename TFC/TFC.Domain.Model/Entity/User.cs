using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TFC.Domain.Model.Enum;

namespace TFC.Domain.Model.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Dni { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
        public Role Role { get; set; } = Role.User;
        public DateTime InscriptionDate { get; set; } = DateTime.UtcNow;
        public List<string> RoutinesIds { get; set; } = new List<string>();
    }
}