using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trading_Cards.Models
{
    public class Card
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("cardName")]
        public string CardName { get; set; } = null!;

        [BsonElement("anime")]
        public string Anime { get; set; } = null!;

        [BsonElement("type")]
        public string Type { get; set; } = null!;

        [BsonElement("rarity")]
        public string Rarity { get; set; } = null!;

        [BsonElement("condition")]
        public string Condition { get; set; } = null!;

        [BsonElement("value")]
        public int Value { get; set; }

        [BsonElement("tags")]
        public List<string>? Tags { get; set; }

        [BsonElement("abilities")]
        public List<string>? Abilities { get; set; }

        [BsonElement("forms")]
        public List<string>? Forms { get; set; }

        [BsonElement("details")]
        public CardDetails? Details { get; set; }
    }

    public class CardDetails
    {
        [BsonElement("set")]
        public string? Set { get; set; }

        [BsonElement("year")]
        public int? Year { get; set; }

        [BsonElement("arc")]
        public string? Arc { get; set; }

        [BsonElement("series")]
        public string? Series { get; set; }

        [BsonElement("powerLevel")]
        public string? PowerLevel { get; set; }
    }
}