namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class EventEditViewModel
    {
        public long RecordId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
    }
}
