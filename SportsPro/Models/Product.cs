using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
    public class Product
    {
		public int ProductID { get; set; }
		[Required(ErrorMessage = "Please enter the product code.")]
		public string ProductCode { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter the name of the product.")]
		public string Name { get; set; } = string.Empty;
		[Column(TypeName = "decimal(8,2)")]

		[Required(ErrorMessage = "Please enter the yearly price.")]
		[Range(0.01, (double)decimal.MaxValue, ErrorMessage= "Yearly price must be greater than 0.")]
		public decimal YearlyPrice { get; set; }

        [Required(ErrorMessage = "Please enter release date.")]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; } = DateTime.Now;

    }
}
