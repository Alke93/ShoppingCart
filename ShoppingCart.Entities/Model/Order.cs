using ShoppingCart.Entities.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Entities.Model
{
    [Table(TableNames.ORDER)]
    public class Order : Entity
    {
        [Column(Order = 2)]
        public int ShoppingCartId { get; set; }

        [Column(Order = 3)]
        public double Amount { get; set; }

        [Column(Order = 4)]
        public double Discount { get; set; }

        [Column(Order = 5)]
        public double Sum { get; set; }

        [Column(Order = 6)]
        [MaxLength(200)]
        public string CustomerAddress { get; set; }

        [Column(Order = 7)]
        [MaxLength(20)]
        public string CustomerPhoneNumber { get; set; }

        [ForeignKey(nameof(ShoppingCartId))]
        public ShoppingCart ShoppingCart { get; set; }
    }
}
