namespace SportsPro.Models
{
    public class IncidentsViewModel
    {
        public String ActiveIncidents { get; set; } = "all";

        public List<Incident> Incidents { get; set; } = new List<Incident>();

    }
}
