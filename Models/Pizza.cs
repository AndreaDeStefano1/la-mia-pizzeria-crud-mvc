using la_mia_pizzeria.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria.Models
{
    public class Pizza
    {
        
        public int PizzaId { get; set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio")]
        [StringLength(25, ErrorMessage = "Il nome non può avere più di 25 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio")]
        [StringLength(255, ErrorMessage = "Il nome non può avere più di 255 caratteri")]
        [MinimumFiveWord]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo descrizione è obbligatorio")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il campo prezzo è obbligatorio")]
        [NotNegativeOrZero]
        public double Price { get; set; }

        public int? CategoryId { get; set; } 
        public Category? Category { get; set; }
        public List<Ingredient>? Ingredients { get; set; }

        public Pizza()
        {
        }

        public Pizza(int id, string name, string description, string image, double price)
        {
            PizzaId = id;
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }
    }
}
