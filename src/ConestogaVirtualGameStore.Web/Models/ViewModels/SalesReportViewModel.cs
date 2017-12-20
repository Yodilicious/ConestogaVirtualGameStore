using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    public class SalesReportViewModel
    {
        public string GameName { get; set; }
        public int NumberOfDownloads { get; set; }
        public double SalesPrice { get; set; }
        public int Year { get; set; }
    }
}
