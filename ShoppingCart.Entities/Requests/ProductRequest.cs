using ShoppingCart.Entities.Attributes;
using System.Runtime.Serialization;

namespace ShoppingCart.Entities.Requests
{
    [ApiEntity]
    public class ProductRequest : IRequest
    {
        [DataMember(EmitDefaultValue = false, Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "quantity")]
        public int Quantity { get; set; }

        public string GetMainTypeName()
        {
            return TableNames.PRODUCT;
        }
    }
}
