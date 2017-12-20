using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    using Models.ViewModels;

    public class MemberListReportController : Controller
    {
        public IActionResult Index()
        {
            var rand = new Random(DateTime.Now.Millisecond);

            var vms = new List<MemberListReportViewModel>();
            for (int i = 0; i < rand.Next(1, 100); i++)
            {
                var vm = new MemberListReportViewModel
                {
                    AccountsCreated = rand.Next(99, 9999),
                    AccountsDeleted = rand.Next(0, 999),
                    Time = DateTime.Now.AddMonths(rand.Next(0, 12) * -1)
                };

                vms.Add(vm);
            }

            return View(vms);
        }
    }
}