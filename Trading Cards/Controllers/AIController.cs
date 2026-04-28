using Microsoft.AspNetCore.Mvc;
using Trading_Cards.Services;

namespace Trading_Cards.Controllers
{
    public class AIController : Controller
    {
        private readonly GeminiService _geminiService;
        private readonly CardService _cardService;

        public AIController(GeminiService geminiService, CardService cardService)
        {
            _geminiService = geminiService;
            _cardService = cardService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ask(string question)
        {
            var cards = _cardService.GetAllCards();

            var answer = await _geminiService.AskGeminiAsync(question, cards);

            ViewBag.Question = question;
            ViewBag.Answer = answer;

            return View("Index");
        }
    }
}