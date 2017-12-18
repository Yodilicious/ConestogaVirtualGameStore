namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class EventCreateViewModel
    {
        public long RecordId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
    }
}
