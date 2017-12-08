namespace ConestogaVirtualGameStore.Web.Models
{
    using System;
    using System.Collections.Generic;

    public class ShoppingCart : BaseModel
    {
        public DateTime PurcheasedOn { get; set; }
        public bool HasPaid { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
