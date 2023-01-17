using System.Runtime.Serialization;

namespace ShoppingCart.Entities.Responses
{
    public class OrderResponse : IResponse
    {
        [DataMember(Name = "orderId")]
        public int OrderId { get; set; }

        [DataMember(Name = "amount")]
        public double Amount { get; set; }

        [DataMember(Name = "discount")]
        public double Discount { get; set; }

        [DataMember(Name = "sum")]
        public double Sum { get; set; }

        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        [DataMember(Name = "customerAddress")]
        public string CustomerAddress { get; set; }

        [DataMember(Name = "customerPhoneNumber")]
        public string CustomerPhoneNumber { get; set; }
    }
}
