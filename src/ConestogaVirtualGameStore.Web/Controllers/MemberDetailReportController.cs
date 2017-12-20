using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    using Data;
    using Models.ViewModels;

    public class MemberDetailReportController : Controller
    {
        private readonly ApplicationDbContext context;

        public MemberDetailReportController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var users = this.context.ApplicationUser.ToList();

            var vms = new List<MemberDetailReportViewModel>();
            foreach (var user in users)
            {
                var vm = new MemberDetailReportViewModel();

                vm.AccountNumber = user.Id;
                vm.MemberName = $"{user.FirstName} {user.LastName}";
                vm.GameDownloads = rand.Next(1, 999);
                vm.TotalPaid = vm.GameDownloads * 49.99;

                vms.Add(vm);
            }

            return View(vms);
        }
    }
}