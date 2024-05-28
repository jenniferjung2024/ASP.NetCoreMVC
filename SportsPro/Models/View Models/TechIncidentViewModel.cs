namespace SportsPro.Models
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; } = null;

        public IEnumerable<Incident> Incidents { get; set; } = null;

    }
}
