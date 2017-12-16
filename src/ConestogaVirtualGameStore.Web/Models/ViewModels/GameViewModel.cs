namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    using System;

    public class GameViewModel
    {
        public long RecordId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string ImageFileName { get; set; }
        public bool IsOwned { get; set; }
    }
}
