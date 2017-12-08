namespace ConestogaVirtualGameStore.Web.Models
{
    using System;
    using System.Collections.Generic;

    public class ShoppingCart : BaseModel
    {
        public string User { get; set; }
        public DateTime PurcheasedOn { get; set; }
        public bool HasPaid { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
