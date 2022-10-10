using la_mia_pizzeria.Models;

public class Ingredient
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public List<Pizza>? pizzas { get; set; }
}
