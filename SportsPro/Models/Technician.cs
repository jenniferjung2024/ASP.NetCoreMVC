using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Technician
    {
		public int TechnicianID { get; set; }

        [Required(ErrorMessage = "Please enter name of technician.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter phone number.")]
        public string Phone { get; set; } = string.Empty;
    }
}
