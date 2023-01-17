using ShoppingCart.Entities;
using ShoppingCart.Entities.Requests;

namespace ShoppingCart.Generics
{
    public interface IService
    {
        Task<ServiceResponse> Execute(IRequest item, SystemAction action);
    }

    public enum SystemAction
    {
        Create,
        Read,
        Update,
        Delete
    }
}
