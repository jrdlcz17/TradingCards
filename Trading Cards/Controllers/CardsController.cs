using Microsoft.AspNetCore.Mvc;
using Trading_Cards.Models;
using Trading_Cards.Services;

namespace Trading_Cards.Controllers
{
    public class CardsController : Controller
    {
        private readonly CardService _cardService;

        public CardsController(CardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult Index()
        {
            var cards = _cardService.GetAllCards();
            return View(cards);
        }

        public IActionResult Details(string id)
        {
            var card = _cardService.GetCardById(id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Card card)
        {
            _cardService.CreateCard(card);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var card = _cardService.GetCardById(id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost]
        public IActionResult Edit(string id, Card card)
        {
            card.Id = id;
            _cardService.UpdateCard(id, card);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var card = _cardService.GetCardById(id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            _cardService.DeleteCard(id);
            return RedirectToAction("Index");
        }
    }
}