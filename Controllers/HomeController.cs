using la_mia_pizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new())
            {

                List<Pizza> pizzaList = db.Pizzas.ToList();
                return View(pizzaList);

            }

        }
    }
}
