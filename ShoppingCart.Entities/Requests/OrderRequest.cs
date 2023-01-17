using ShoppingCart.Entities.Attributes;
using System.Runtime.Serialization;

namespace ShoppingCart.Entities.Requests
{
    [ApiEntity]
    public class OrderRequest : IRequest
    {
        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        public string GetMainTypeName()
        {
           return TableNames.ORDER;
        }
    }
}
