using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsPro.Models
{
    public class Incident
    {
		public int IncidentID { get; set; }

		[Required(ErrorMessage = "Please enter a title for the incident.")]
		public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description for the incident.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select the date the incident is opened.")]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)] public DateTime? DateOpened { get; set; } = DateTime.Now;


        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DateClosed { get; set; } = null;

        [Required(ErrorMessage = "Please select a customer from the drop-down menu.")]
        public int? CustomerID { get; set; }                   // foreign key property

        [ValidateNever]
        public Customer Customer { get; set; } = null!;       // navigation property

        [Required(ErrorMessage = "Please select a product from the drop-down menu.")]
        public int? ProductID { get; set; }                    // foreign key property

        [ValidateNever]
        public Product Product { get; set; } = null!;         // navigation property

        [Required(ErrorMessage = "Please select a technician from the drop-down menu.")]
        public int? TechnicianID { get; set; }                 // foreign key property 

        [ValidateNever]
        public Technician Technician { get; set; } = null!;   // navigation property

		
	}
}