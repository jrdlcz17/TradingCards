using MongoDB.Driver;
using Trading_Cards.Models;

namespace Trading_Cards.Services
{
    public class CardService
    {
        private readonly IMongoCollection<Card> _cards;

        public CardService(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDBSettings:ConnectionString"];
            var databaseName = configuration["MongoDBSettings:DatabaseName"];
            var collectionName = configuration["MongoDBSettings:CollectionName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _cards = database.GetCollection<Card>(collectionName);
        }

        public List<Card> GetAllCards()
        {
            return _cards.Find(card => true).ToList();
        }

        public Card? GetCardById(string id)
        {
            return _cards.Find(card => card.Id == id).FirstOrDefault();
        }

        public void CreateCard(Card card)
        {
            _cards.InsertOne(card);
        }

        public void UpdateCard(string id, Card updatedCard)
        {
            _cards.ReplaceOne(card => card.Id == id, updatedCard);
        }

        public void DeleteCard(string id)
        {
            _cards.DeleteOne(card => card.Id == id);
        }
    }
}