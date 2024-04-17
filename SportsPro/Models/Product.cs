using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
		public int ProductID { get; set; }
		public string ProductCode { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		[Column(TypeName = "decimal(8,2)")]
		public decimal YearlyPrice { get; set; }
		public DateTime ReleaseDate { get; set; } = DateTime.Now;
	}
}
