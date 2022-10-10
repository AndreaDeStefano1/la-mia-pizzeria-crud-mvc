namespace la_mia_pizzeria.Models
{
    public class CategoryPizza
    {
        
        public Pizza Pizza { get; set; }

        public List<Category>? Categories { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<int> IngredientIds { get; set; }

       
    }
    
}
