using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public TechIncidentController(SportsProContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Index()
        {
            var technician = new Technician();
            int? technicianID = HttpContext.Session.GetInt32("techID");
            if (technicianID.HasValue)
            {
                technician = context.Technicians.Find(technicianID);
            }

            var technicians = context.Technicians.OrderBy(t => t.Name).Where(t => t.TechnicianID > -1).ToList();

            foreach (var t in technicians)
            {
                Console.WriteLine(t.TechnicianID + " " + t.Name);
            }

            ViewBag.Technicians = technicians;
            return View(technician);
        }

        [HttpPost]

        public IActionResult List(Technician technican)
        {
            if (technican.TechnicianID == 0) 
            {
                TempData["message"] = "You must select a technician.";
                return RedirectToAction("Index");
            }

            else
            {
                HttpContext.Session.SetInt32("techID", technican.TechnicianID);
                return RedirectToAction("List");

            }
        }


        [HttpGet]
        public IActionResult List()
        {
            int id = (int)HttpContext.Session.GetInt32("techID");
            var techIncident = new TechIncidentViewModel();

            techIncident.Technician = context.Technicians.Find(id);

            var incidents = context.Incidents.Include(i => i.Technician).Include(i => i.Customer).Include(i => i.Product).OrderBy(i => i.DateOpened).Where(i => i.TechnicianID == id).Where(i => i.DateClosed == null).ToList();

            techIncident.Incidents = incidents;

            if (techIncident.Technician == null)
            {
                TempData["message"] = "Technician not found.  Please select a technician.";
                return RedirectToAction("Index");
            }

            else
            {
                Console.WriteLine("Technician: " + techIncident.Technician.TechnicianID + "-" + techIncident.Technician.Name);

                foreach (var i in incidents)
                {
                    Console.WriteLine("Incident ID: " + i.IncidentID + "\nCustomerID: " + i.Customer.FullName + "\nProductID: " + i.Product.Name + "\nDate Opened: " + i.DateOpened + "\nTitle: " + i.Title);
                }

                return View(techIncident);
            }
 
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            int? technicianID = HttpContext.Session.GetInt32("techID");
            if (!technicianID.HasValue)
            {
                TempData["message"] = "Technician not found.  Please select a technician.";
                return RedirectToAction("Index");
            }

            Technician technician = context.Technicians.Find(technicianID);

            if (technician == null)
            {
                TempData["message"] = "Technician not found.  Please select a technician.";
                return RedirectToAction("Index");
            }

            else
            {
                Incident model = context.Incidents.Include(t => t.Technician).Include(p => p.Product).Include(c => c.Customer).FirstOrDefault(i => i.IncidentID == id) ?? new Incident();
                return View(model);
            }

            // ViewBag.Customers = context.Customers.ToList();
            // ViewBag.Products = context.Products.ToList();
            // ViewBag.Technicians = context.Technicians.ToList().Except(context.Technicians.Where(x => x.TechnicianID == -1));
            // var incident = context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product).Where(x => x.IncidentID == id).First();
            // return View(incident);
        }


        [HttpPost]
        public IActionResult Edit(Incident editedIncident)
        {
            context.Incidents.Update(editedIncident);
            context.SaveChanges();
            int? techID = HttpContext.Session.GetInt32("techID");

            return RedirectToAction("List", new { id = techID });
        }



    }
}
