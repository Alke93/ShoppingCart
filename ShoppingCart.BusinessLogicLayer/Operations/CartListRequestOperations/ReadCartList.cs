using Microsoft.Extensions.Configuration;
using ShoppingCart.DataAccessLayer.Interfaces;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Requests;
using ShoppingCart.Entities.Responses;

namespace ShoppingCart.BusinessLogicLayer.Operations.CartListRequestOperations
{
    internal class ReadCartList : GeneralSystemOperation
    {
        public ReadCartList() { }

        public ReadCartList(IReadOnlyRepository readOnlyRepo, IRepository repo, IConfiguration configuration) : base(readOnlyRepo, repo, configuration)
        {

        }

        protected override async Task<ServiceResponse> ConcreteOperation(object request)
        {
            var cartRequest = (CartListRequest)request;
            var cart = await ReadOnlyRepository.GetFirstAsync<Entities.Model.ShoppingCart>(item => item.CustomerId == cartRequest.CustomerId && !item.IsCompleted, null, "ShoppingItems,ShoppingItems.Product");
            if (cart == null)
            {
                return ResponseHelper.ItemNotFound("Shopping cart empty");
            }

            var response = cart.ShoppingItems.Select(item => new CartListResponse()
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
            }).ToList();

            return ResponseHelper.SuccessfulResponse(response, "Cart list found");
        }

        protected override async Task<bool> Validate(object request)
        {
            var item = (CartListRequest)request;
            return await Task.Run(() => item.CustomerId > 0);
        }
    }
}
