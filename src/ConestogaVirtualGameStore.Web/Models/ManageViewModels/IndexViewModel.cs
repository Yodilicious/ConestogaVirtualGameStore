using System.ComponentModel.DataAnnotations;

namespace ConestogaVirtualGameStore.Web.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string AddressA { get; set; }

        [Display(Name = "Address Other")]
        public string AddressB { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Name on Credit Card")]
        public string NameOnCreditCard { get; set; }

        [Display(Name = "Credit Card Number")]
        public string CreditCard { get; set; }

        [Display(Name = "Expiry Month")]
        public string ExpiryMonth { get; set; }

        [Display(Name = "Expiry Year")]
        public string ExpiryYear { get; set; }

        [Display(Name = "CCV Code")]
        public string Ccv { get; set; }
    }
}
