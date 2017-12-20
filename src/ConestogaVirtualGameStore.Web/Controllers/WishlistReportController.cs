using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    using Data;
    using Models.ViewModels;

    public class WishlistReportController : Controller
    {
        private readonly ApplicationDbContext context;

        public WishlistReportController(ApplicationDbContext context)
        {
            this.context = context; 
        }

        public IActionResult Index()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var games = this.context.Games.ToList();

            var vms = new List<WishlistReportViewModel>();
            foreach (var game in games)
            {
                var vm = new WishlistReportViewModel();

                vm.GameName = game.Title;
                vm.WishlistSaves = rand.Next(0, 499);
                vm.PopulatiryRating = rand.Next(1, 5);

                vms.Add(vm);
            }

            return View(vms);
        }
    }
}