namespace ConestogaVirtualGameStore.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class ShoppingCartViewModel
    {
        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressA { get; set; }
        public string AddressB { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string CreditCard { get; set; }
        public string CreditCardName { get; set; }
        public string Ccv { get; set; }
        public string CreditCardMonth { get; set; }
        public string CreditCardYear { get; set; }
    }
}
