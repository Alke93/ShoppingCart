using ShoppingCart.Entities.Generics;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Entities.Model
{
    [Table(TableNames.SHOPPINGCART)]
    public class ShoppingCart : Entity
    {
        [Column(Order = 2)]
        public int CustomerId { get; set; }
        [Column(Order = 3)]
        public bool IsCompleted { get; set; }

        public List<ShoppingItem> ShoppingItems { get; set; }
    }
}
