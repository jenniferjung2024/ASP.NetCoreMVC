using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
        {
        private SportsProContext context { get; set; }

        public CustomerController(SportsProContext ctx) => context = ctx;

        [Route("[controller]s")]
        public IActionResult List()
        {
            var customers = context.Customers.OrderBy(c => c.LastName).ToList();
            return View(customers);
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.ToList();
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.ToList();
            var customer = context.Customers.Find(id);
            return View(customer);
        }


       

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                    context.Customers.Add(customer);
                else
                    context.Customers.Update(customer);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Countries = context.Countries.ToList();
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                return View(customer);
            }
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
