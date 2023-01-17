using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ShoppingCart.Entities.Generics
{
    public enum State
    {
        New = 3,
        Modified = 2,
        Deleted = 1,
        ReadOnly = 0
    }

    [DataContract]
    public abstract class Entity : TEntityType
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }
        [NotMapped]
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public State EntityState { get; set; }

    }
}
