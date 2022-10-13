using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;


namespace la_mia_pizzeria.Controllers
{
    [Authorize]
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
                List<Pizza> pizze = db.Pizzas.OrderByDescending(pizza => pizza.PizzaId).ToList<Pizza>();
                return View(pizze);
            }
            
        }
        public IActionResult Show(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza p = db.Pizzas.Where(s => s.PizzaId == id).Include("Category").Include("Ingredients").First<Pizza>();
                return View(p);

            }

        }
        public IActionResult Create()
        {
            using (PizzaContext db = new())
            {
                CategoryPizza pizzaCategories = new CategoryPizza();

             
                pizzaCategories.Categories = db.Categories.ToList();
                pizzaCategories.Ingredients = db.Ingredients.ToList();

                return View(pizzaCategories);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryPizza formData)
        {
            using (PizzaContext db = new PizzaContext())
            {
                if (!ModelState.IsValid)
                {
                    //CategoryPizza c = new();
                    //c.Pizza = pizza;
                    //c.Categories = db.Categories.ToList();
                    formData.Categories = db.Categories.ToList();
                    formData.Ingredients = db.Ingredients.ToList();
                    return View("Create", formData);
                }

                Pizza p = new Pizza();
                p.Name = formData.Pizza.Name;
                p.Description = formData.Pizza.Description;
                p.Image = formData.Pizza.Image;
                p.Price = formData.Pizza.Price;
                p.CategoryId = formData.Pizza.CategoryId;

                
                    
                p.Ingredients = db.Ingredients.Where(i => formData.IngredientIds.Contains(i.Id)).ToList();  
                

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
                Pizza p = db.Pizzas.Where(pizza => pizza.PizzaId == id).Include("Category").Include("Ingredients").First();
                List<Category> category = db.Categories.ToList();
                CategoryPizza pizzaECategorie = new CategoryPizza();
                pizzaECategorie.Pizza = p;
                pizzaECategorie.Categories = category;
                pizzaECategorie.Ingredients = db.Ingredients.ToList();
                return View(pizzaECategorie);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, CategoryPizza formData)
        {
            using (PizzaContext db = new PizzaContext())
            {
                if (!ModelState.IsValid)
                {
                    formData.Categories = db.Categories.ToList();
                    formData.Ingredients = db.Ingredients.ToList();
                    formData.Pizza.Ingredients = db.Ingredients.Where(i => formData.IngredientIds.Contains(i.Id)).ToList();
                    formData.Pizza.PizzaId = id;
                    return View("Update", formData);
                }
                Pizza p = db.Pizzas.Include("Ingredients").Where(pizza => pizza.PizzaId == id).First();
                p.Name = formData.Pizza.Name;
                p.Description = formData.Pizza.Description;
                p.Image = formData.Pizza.Image;
                p.Price = formData.Pizza.Price;
                p.CategoryId = formData.Pizza.CategoryId;
                p.Ingredients = db.Ingredients.Where(i => formData.IngredientIds.Contains(i.Id)).ToList();
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
