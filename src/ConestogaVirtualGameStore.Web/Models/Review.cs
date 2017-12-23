namespace ConestogaVirtualGameStore.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review : BaseModel
    {
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }

        public long GameId { get; set; }
        public Game Game { get; set; }
    }
}
