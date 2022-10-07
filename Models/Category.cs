using la_mia_pizzeria.Models;

public class Category
{


    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<Pizza> Pizzas { get; set; }

    public Category()
    {
    }
}