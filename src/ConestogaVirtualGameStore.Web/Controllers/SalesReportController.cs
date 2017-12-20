using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    using Data;
    using Models.ViewModels;

    public class SalesReportController : Controller
    {
        private readonly ApplicationDbContext context;

        public SalesReportController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var games = this.context.Games.ToList();

            var vms = new List<SalesReportViewModel>();
            foreach (var game in games)
            {
                var vm = new SalesReportViewModel();

                vm.GameName = game.Title;
                vm.NumberOfDownloads = rand.Next(99, 999);
                vm.SalesPrice = vm.NumberOfDownloads * 79.99;
                vm.Year = DateTime.Now.Year;

                vms.Add(vm);
            }

            return View(vms);
        }
    }
}