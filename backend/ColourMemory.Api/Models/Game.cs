using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ColourMemory.Api.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<Card> Cards { get; set; }
        public int Score { get; set; }
        public bool IsGameOver { get; set; }
    }
}
