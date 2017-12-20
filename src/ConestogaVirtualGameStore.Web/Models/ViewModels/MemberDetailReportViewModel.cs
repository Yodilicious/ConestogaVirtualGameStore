using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    public class MemberDetailReportViewModel
    {
        public string AccountNumber { get; set; }
        public string MemberName { get; set; }
        public int GameDownloads { get; set; }
        public double TotalPaid { get; set; }
    }
}
