using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    public class GameDetailReportViewModel
    {
        public string GameName { get; set; }
        public int DownloadCount { get; set; }
        public int DeletedCount { get; set; }
    }
}
