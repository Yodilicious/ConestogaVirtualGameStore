namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class GameEditViewModel
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

        public IFormFile File { get; set; }

        public bool IsDownloadable { get; set; }
    }
}
