﻿namespace ConestogaVirtualGameStore.Presentation.Models
{
    using System;

    public class Review : BaseModel
    {
        public Guid Author { get; set; }
        public string Title { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }

        public long GameId { get; set; }
        public Game Game { get; set; }
    }
}
