using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    public class GameCreateViewModel
    {
        public long RecordId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string ImageFileName { get; set; }
        public IFormFile File { get; set; }
        public bool IsDownloadable { get; set; }
    }
}
