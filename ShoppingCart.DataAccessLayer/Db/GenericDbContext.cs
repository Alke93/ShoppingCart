using Microsoft.EntityFrameworkCore;
using ShoppingCart.DataAccessLayer.Interfaces;
using ShoppingCart.Entities.Model;

namespace ShoppingCart.DataAccessLayer.Db
{
    public class GenericDbContext : DbContext, IGenericContext
    {
        public GenericDbContext(DbContextOptions<GenericDbContext> options)
               : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(item => item.ShoppingCart);
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<ShoppingItem>().HasOne(item => item.Product);
            modelBuilder.Entity<Entities.Model.ShoppingCart>().HasMany(item => item.ShoppingItems);
        }

    }
}
