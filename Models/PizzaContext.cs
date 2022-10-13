using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Models
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public PizzaContext()
        {

        }

        public PizzaContext(DbContextOptions<PizzaContext> options)
        : base(options)
        {
        }

        public DbSet<Pizza> Pizzas { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True");
        }
    }
}
