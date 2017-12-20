using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    public class EmployeeEarningsReportViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double HourlyWage { get; set; }
        public double Total { get; set; }
        public string Month { get; set; }
    }
}
