using System.Text;
using System.Text.Json;
using Trading_Cards.Models;

namespace Trading_Cards.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> AskGeminiAsync(string question, List<Card> cards)
        {
            string apiKey = _configuration["GeminiSettings:ApiKey"]!;
            string model = _configuration["GeminiSettings:Model"]!;

            string cardData = string.Join("\n", cards.Select(card =>
                $"Name: {card.CardName}, Anime: {card.Anime}, Type: {card.Type}, Rarity: {card.Rarity}, Value: {card.Value}"
            ));

            string prompt = $@"
You are helping answer questions about a trading card collection.

User question:
{question}

Trading card data from MongoDB:
{cardData}

Answer the user's question using only the card data provided.
";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string url = $"https://generativelanguage.googleapis.com/v1/models/{model}:generateContent?key={apiKey}";

            var response = await _httpClient.PostAsync(url, content);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return "Gemini API error: " + responseText;
            }

            using JsonDocument doc = JsonDocument.Parse(responseText);

            return doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "No response from Gemini.";
        }
    }
}