using System.Runtime.Serialization;

namespace ShoppingCart.Entities
{
    [DataContract]
    public class ServiceResponse
    {
        [DataMember(EmitDefaultValue = false, Name = "status")]
        public ResponseStatus Status { get; set; }
        [DataMember(EmitDefaultValue = false, Name = "description")]
        public string Description { get; set; }
        [DataMember(EmitDefaultValue = false, Name = "body")]
        public object Body { get; set; }
    }

    public enum ResponseStatus
    {
        Ok = 200,
        ServerError = 500,
        ModelNotValid = 300,
        ItemNotFound = 301,
        OperationNotFound = 404
    }
}
