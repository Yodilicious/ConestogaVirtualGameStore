using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    public class MemberListReportViewModel
    {
        public int AccountsCreated { get; set; }
        public int AccountsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
