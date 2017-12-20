using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    using Data;
    using Models.ViewModels;

    public class GameDetailReportController : Controller
    {
        private readonly ApplicationDbContext contxt;

        public GameDetailReportController(ApplicationDbContext context)
        {
            this.contxt = context;
        }

        public IActionResult Index()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var games = this.contxt.Games.ToList();

            var vms = new List<GameDetailReportViewModel>();
            foreach (var game in games)
            {
                var vm = new GameDetailReportViewModel();

                vm.GameName = game.Title;
                vm.DownloadCount = rand.Next(5, 9999);
                vm.DeletedCount = rand.Next(0, 999);

                vms.Add(vm);
            }

            return View(vms);
        }
    }
}