using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportsPro.Models;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private SportsProContext context {  get; set; }

        public ProductController(SportsProContext ctx) => context = ctx;

        [Route("[controller]s")]
        public ViewResult List()
        {
            var products = context.Products.OrderBy(p => p.ReleaseDate).ToList();
            ViewBag.Message = TempData["message"];
            return View(products);
        }


        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = context.Products.Find(id);
            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid) {
                if (product.ProductID == 0)
                {
                    context.Products.Add(product);
                    TempData["message"] = $"{product.Name} was added.";
                }
                else
                {
                    context.Products.Update(product);
                    TempData["message"] = $"{product.Name} was edited.";
                }
                context.SaveChanges();
                return RedirectToAction("List");
                }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                return View(product);
            }
        }



        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = context.Products.Find(id);
            TempData["product_name"] = product.Name;
            return View(product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            TempData["message"] = TempData["product_name"] + " was deleted.";
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
