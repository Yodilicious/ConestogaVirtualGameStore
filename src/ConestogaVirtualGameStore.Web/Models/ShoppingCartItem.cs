namespace ConestogaVirtualGameStore.Web.Models
{
    using System;

    public class ShoppingCartItem : BaseModel
    {
        public long ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public long GameId { get; set; }
        public Game Game { get; set; }

        public decimal Price { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
