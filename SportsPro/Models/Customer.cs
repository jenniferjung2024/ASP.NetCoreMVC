namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string State { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string? Phone { get; set; } 
        public string? Email { get; set; } 

        public string CountryID { get; set; } = string.Empty;   // foreign key property
        public Country Country { get; set; } = null!;           // navigation property

        public string FullName => FirstName + " " + LastName;   // read-only property
	}
}