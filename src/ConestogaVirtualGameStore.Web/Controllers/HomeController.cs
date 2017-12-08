namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repository;

    public class HomeController : Controller
    {
        private readonly IGameRepository gameRepository;

        public HomeController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public IActionResult Index()
        {
            Game[,] games = new Game[3, 3];

            var gamesArray = this.gameRepository.GetLastNineGames().ToArray();
            var index = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    games[i, j] = gamesArray[index];
                    index++;
                }
            }

            return View(games);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
