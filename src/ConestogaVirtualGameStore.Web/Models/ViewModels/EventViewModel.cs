namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    using System;

    public class EventViewModel
    {
        public long RecordId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string ImagePath { get; set; }
        public bool IsRegistered { get; set; }
    }
}
