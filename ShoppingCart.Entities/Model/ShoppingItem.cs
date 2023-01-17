using ShoppingCart.Entities.Generics;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Entities.Model
{
    [Table(TableNames.SHOPPINGITEM)]
    public class ShoppingItem : Entity
    {
        [Column(Order = 2)]
        public int ShoppingCartId { get; set; }
        [Column(Order = 3)]
        public int ProductId { get; set; }

        [Column(Order = 4)]
        public int Quantity { get; set; }

        [Column(Order = 5)]
        public double UnitPrice { get; set; }

        [Column(Order = 6)]
        public double TotalPrice { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
    }
}
