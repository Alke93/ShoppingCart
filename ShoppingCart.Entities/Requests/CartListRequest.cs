using ShoppingCart.Entities.Attributes;
using System.Runtime.Serialization;

namespace ShoppingCart.Entities.Requests
{
    [ApiEntity]
    public class CartListRequest : IRequest
    {
        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }
        public string GetMainTypeName()
        {
            return "CartList";
        }
    }
}