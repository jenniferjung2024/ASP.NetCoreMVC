using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncidentController : Controller
    {
        private SportsProContext Context { get; set; }

        public IncidentController(SportsProContext ctx) => Context = ctx;

        [Route("[controller]s")]

        public IActionResult List()
        {
            var incident = Context.Incidents.Include(c => c.Customer).Include(p => p.Product).OrderBy(i => i.Title).ThenBy(i => i.DateOpened).ToList();
            return View(incident);
        }

        public IActionResult List(IncidentsViewModel model)
        {
        model.Incidents = Context.Incidents.Include(c => c.Customer).Include(p => p.Product).OrderBy(i => i.Title).ThenBy(i => i.DateOpened).ToList();

        IQueryable<Incident> query = Context.Incidents.Include(c => c.Customer).Include(p => p.Product).OrderBy(i => i.Title).ThenBy(i => i.DateOpened);

            if (model.ActiveIncidents == "unassigned")
                {
                model.Incidents = (List<Incident>)query.Where(incident => incident.TechnicianID == -1);
                }


            else if (model.ActiveIncidents == "open")
                {
                model.Incidents = (List<Incident>)query.Where(incident => incident.DateClosed == null);
                }


        model.Incidents = query.ToList();

        return View(model);

        }


        [HttpGet]
        public IActionResult Add(object exclude)
        {
            ViewBag.Action = "Add";
            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.ToList();
            ViewBag.Technicians = Context.Technicians.ToList().Except(Context.Technicians.Where(x => x.TechnicianID == -1));
            return View("Edit", new Incident());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            ViewBag.Customers = Context.Customers.ToList();
            ViewBag.Products = Context.Products.ToList();
            ViewBag.Technicians = Context.Technicians.ToList().Except(Context.Technicians.Where(x => x.TechnicianID == -1));
            var incident = Context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product).Where(x => x.IncidentID == id).First();
            return View(incident);
        }


        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                    Context.Incidents.Add(incident);
                else
                    Context.Incidents.Update(incident);
                Context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = (incident.IncidentID == 0) ? "Add" : "Edit";
                ViewBag.Customers = Context.Customers.ToList();
                ViewBag.Products = Context.Products.ToList();
                ViewBag.Technicians = Context.Technicians.ToList().Except(Context.Technicians.Where(x => x.TechnicianID == -1));
                Context.Incidents.Include(c => c.Customer).Include(t => t.Technician).Include(p => p.Product);
                return View(incident);
            }
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = Context.Incidents.Include(c => c.Customer).Where(x => x.IncidentID == id).First();
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            Context.Incidents.Include(c => c.Customer);
            Context.Incidents.Remove(incident);
            Context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
