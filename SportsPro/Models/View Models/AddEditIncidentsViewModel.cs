namespace SportsPro.Models
{
    public class AddEditIncidentsViewModel
    {

        public Incident CurrIncident { get; set; } = new Incident();

        public String CurrOperation { get; set; } = "add";

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Product> Products { get; set;} = new List<Product>();

        public List<Technician> Technicians { get; set; } = new List<Technician>();

    }


}
