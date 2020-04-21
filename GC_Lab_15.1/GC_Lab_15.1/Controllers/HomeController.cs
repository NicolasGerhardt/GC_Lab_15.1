using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GC_Lab_15._1.Models;
using System.Net.Http;

namespace GC_Lab_15._1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // How to get a shuffled deck: https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1
            // How to draw x number of cards from Deck https://deckofcardsapi.com/api/deck/<<deck_id>>/draw/?count=

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");
            // some API's need to have headers set for them. 
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla / 5.0 (compatible, GrandCircus/1.0)");

            var response = await client.GetAsync("api/deck/new/shuffle/?deck_count=1");

            var deck = await response.Content.ReadAsAsync<Deck>();

            var response2 = await client.GetAsync($"api/deck/{deck.Deck_id}/draw/?count=5");

            var cards = await response2.Content.ReadAsAsync<CardsList>();

            return View(cards);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
