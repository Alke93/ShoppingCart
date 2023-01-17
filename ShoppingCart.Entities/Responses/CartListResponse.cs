using System.Runtime.Serialization;

namespace ShoppingCart.Entities.Responses
{
    public class CartListResponse : IResponse
    {
        [DataMember(EmitDefaultValue = false, Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "productName")]
        public string ProductName { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "unitPrice")]
        public double UnitPrice { get; set; }
    }
}
