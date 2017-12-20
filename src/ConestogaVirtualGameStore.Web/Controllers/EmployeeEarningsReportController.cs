using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    using ConestogaVirtualGameStore.Web.Models.ViewModels;
    using Data;

    public class EmployeeEarningsReportController : Controller
    {
        private readonly ApplicationDbContext context;

        public EmployeeEarningsReportController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var users = this.context.ApplicationUser.ToList();

            var vms = new List<EmployeeEarningsReportViewModel>();
            foreach (var user in users)
            {
                var vm = new EmployeeEarningsReportViewModel();

                vm.Name = $"{user.FirstName} {user.LastName}";
                vm.Id = user.Id;
                vm.HourlyWage = (rand.NextDouble() * 49) + 9.99;
                vm.Total = rand.Next(1, 24) * 35 * 4.5 * vm.HourlyWage;
                vm.Month = DateTime.Now.ToString("MMMM");

                vms.Add(vm);
            }

            return View(vms);
        }
    }
}