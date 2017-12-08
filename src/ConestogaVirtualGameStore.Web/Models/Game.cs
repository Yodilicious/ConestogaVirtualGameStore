namespace ConestogaVirtualGameStore.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string ImageFileName { get; set; }

        public IList<Review> Reviews { get; set; }
        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
