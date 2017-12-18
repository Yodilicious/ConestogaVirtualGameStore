namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.ViewModels;
    using Repository;

    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameRepository gameRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GameController(IGameRepository gameRepository, IHostingEnvironment hostingEnvironment)
        {
            this.gameRepository = gameRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(string id)
        {
            List<GameViewModel> games = null;
            List<Game> data = null;
            List<Game> myGames = null;

            if (!string.IsNullOrEmpty(id))
            {
                games = new List<GameViewModel>();
                data = this.gameRepository.GetGames(id);
                myGames = this.gameRepository.GetMyGames(User.Identity.Name);

                foreach (var game in data)
                {
                    var vm = new GameViewModel();

                    vm.RecordId = game.RecordId;
                    vm.Title = game.Title;
                    vm.Description = game.Description;
                    vm.Date = game.Date;
                    vm.Developer = game.Developer;
                    vm.ImageFileName = game.ImageFileName;
                    vm.Price = game.Price;
                    vm.Publisher = game.Publisher;
                    vm.IsOwned = myGames.Exists(g => g.RecordId == game.RecordId);

                    games.Add(vm);
                }
            }
            else
            {
                games = new List<GameViewModel>();
                data = this.gameRepository.GetGames();
                myGames = this.gameRepository.GetMyGames(User.Identity.Name);

                foreach (var game in data)
                {
                    var vm = new GameViewModel();

                    vm.RecordId = game.RecordId;
                    vm.Title = game.Title;
                    vm.Description = game.Description;
                    vm.Date = game.Date;
                    vm.Developer = game.Developer;
                    vm.ImageFileName = game.ImageFileName;
                    vm.Price = game.Price;
                    vm.Publisher = game.Publisher;
                    vm.IsOwned = myGames.Exists(g => g.RecordId == game.RecordId);

                    games.Add(vm);
                }
            }

            return View(games);
        }
        
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = this.gameRepository.GetGame(id.Value);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Price,Date,Developer,Publisher,RecordId,File")] GameCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var game = new Game();

                game.Title = vm.Title;
                game.Description = vm.Description;
                game.Price = vm.Price;
                game.Date = vm.Date;
                game.Developer = vm.Developer;
                game.Publisher = vm.Publisher;
                game.RecordId = vm.RecordId;

                var file = vm.File;
                var parsedContentDisposition =
                    ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var filename = Path.Combine(_hostingEnvironment.WebRootPath, "images", "games", parsedContentDisposition.FileName.Trim('"'));
                using (var stream = System.IO.File.OpenWrite(filename))
                {
                    file.CopyTo(stream);
                }

                game.ImageFileName = parsedContentDisposition.FileName.Trim('"');
                this.gameRepository.AddGame(game);
                this.gameRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public IActionResult CreateReview(long id)
        {
            HttpContext.Session.SetInt32("game_id", (int)id);

            return RedirectToAction("Create", "Reviews");
        }

        // GET: Game/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = this.gameRepository.GetGame(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Title,Description,Price,Date,Developer,Publisher,RecordId")] Game game)
        {
            if (id != game.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    game.ImageFileName = String.Empty;
                    this.gameRepository.UpdateGame(game);
                    this.gameRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.RecordId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Game/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = this.gameRepository.GetGame(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var game = this.gameRepository.GetGame(id);

            this.gameRepository.RemoveGame(game);
            this.gameRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ConfirmShoppingCart(long id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var game = this.gameRepository.AddGameToShoppingCart(id, User.Identity.Name);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public IActionResult AddGameToWishList(long id)
        {
            this.gameRepository.AddGameToWishlist(id, User.Identity.Name);
            
            return RedirectToAction("Index", "Wishlist", new { @id = string.Empty });
        }

        private bool GameExists(long id)
        {
            return this.gameRepository.Exists(id);
        }
    }
}