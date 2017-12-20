using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    public class WishlistReportViewModel
    {
        public string GameName { get; set; }
        public int WishlistSaves { get; set; }
        public int PopulatiryRating { get; set; }
    }
}
