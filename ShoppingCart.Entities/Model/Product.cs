using ShoppingCart.Entities.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Entities.Model
{
    [Table(TableNames.PRODUCT)]
    public class Product : Entity
    {
        [Column(Order = 2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(Order = 3)]
        public int Quantity { get; set; }

        [Column(Order = 4)]
        public int ReservedQuantity { get; set; }
        
        [Column(Order = 5)]
        public double Price { get; set; }
    }
}