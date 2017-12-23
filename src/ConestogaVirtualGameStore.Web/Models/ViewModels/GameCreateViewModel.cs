using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class GameCreateViewModel
    {
        public long RecordId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Publisher { get; set; }

        public string ImageFileName { get; set; }

        [Required]
        public IFormFile File { get; set; }

        public bool IsDownloadable { get; set; }
    }
}
