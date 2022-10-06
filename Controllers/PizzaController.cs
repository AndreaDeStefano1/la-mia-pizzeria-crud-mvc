using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;


namespace la_mia_pizzeria.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizze = db.Pizzas.OrderByDescending(pizza => pizza.Id).ToList<Pizza>();
                return View(pizze);
            }
            
        }
        public IActionResult Show(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = db.Pizzas.Where(s => s.Id == id).First<Pizza>();
                return View(p);

            }

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = new Pizza();
                p.Name = pizza.Name;
                p.Description = pizza.Description;
                p.Image = pizza.Image;
                p.Price = pizza.Price;
                db.Add(p);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using(PizzaContext db = new PizzaContext())
            {
                Pizza p = db.Pizzas.Find(id);
                return View(p);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza formData)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = db.Pizzas.Find(id);
                p.Name = formData.Name;
                p.Description = formData.Description;
                p.Image = formData.Image;
                p.Price = formData.Price;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = db.Pizzas.Find(id);
                
                db.Pizzas.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}