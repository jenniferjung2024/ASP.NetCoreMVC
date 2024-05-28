using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "Please enter first name.")]
		public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter street address.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter city.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter state.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter zip code.")]
        public string PostalCode { get; set; } = string.Empty;
		public string? Phone { get; set; } 
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please select a country from the drop-down list.")]
        public string CountryID { get; set; } = string.Empty;   // foreign key property

        [ValidateNever]
        public Country Country { get; set; } = null!;           // navigation property

        public string FullName => FirstName + " " + LastName;   // read-only property
	}
}