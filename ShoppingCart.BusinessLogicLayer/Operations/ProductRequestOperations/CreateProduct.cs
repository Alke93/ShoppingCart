using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ShoppingCart.DataAccessLayer.Interfaces;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Model;
using ShoppingCart.Entities.Requests;
using System.Net;
using System.Net.Http.Json;

namespace ShoppingCart.BusinessLogicLayer.Operations.ProductRequestOperations
{
    internal class CreateProduct : GeneralSystemOperation
    {
        public CreateProduct() { }

        public CreateProduct(IReadOnlyRepository readOnlyRepo, IRepository repo, IConfiguration configuration) : base(readOnlyRepo, repo, configuration)
        {

        }
        protected override async Task<ServiceResponse> ConcreteOperation(object request)
        {
            var productRequest = (ProductRequest)request;
            var product = await ReadOnlyRepository.GetFirstAsync<Product>(p => p.Id == productRequest.ProductId);
            if (product == null)
            {
                return ResponseHelper.ItemNotFound("Product not found");
            }

            var realQuantity = product.Quantity - product.ReservedQuantity;
            if (realQuantity < productRequest.Quantity)
            {
                var mocked = await this.getMockedQuantity(productRequest.ProductId);
                if (mocked == null || (mocked.Quantity + realQuantity) < productRequest.Quantity)
                {
                    return new ServiceResponse() { Description = "Product out of stock" };
                }
            }

            var shoppingList = await ReadOnlyRepository.GetFirstAsync<Entities.Model.ShoppingCart>(item => item.CustomerId == productRequest.CustomerId && !item.IsCompleted, null, "ShoppingItems");
            if (shoppingList == null)
            {
                shoppingList = new Entities.Model.ShoppingCart()
                {
                    CustomerId = productRequest.CustomerId,
                    IsCompleted = false
                };

                Repository.Create(shoppingList);
            }
            else
            {
                Repository.ChangeState(shoppingList, Entities.Generics.State.ReadOnly);
                var itemExists = shoppingList.ShoppingItems.FirstOrDefault(item => item.ProductId == productRequest.ProductId);
                if (itemExists != null)
                {
                    return new ServiceResponse() { Description = "Product already added to list" };
                }
            }

            var shoppingItem = new ShoppingItem() { ProductId = productRequest.ProductId, ShoppingCart = shoppingList, ShoppingCartId = shoppingList.Id, Quantity = productRequest.Quantity, UnitPrice = product.Price, TotalPrice = product.Price * productRequest.Quantity };
            Repository.Create(shoppingItem);
            product.ReservedQuantity += productRequest.Quantity;
            Repository.Update(product);
            await Repository.SaveAsync();

            return ResponseHelper.SuccessfulResponse(null, "Product added successfully to shopping list");
        }

        protected override async Task<bool> Validate(object request)
        {
            var item = (ProductRequest)request;
            return await Task.Run(() => item.CustomerId > 0 && item.ProductId > 0 && item.Quantity > 0);
        }

        private async Task<Product> getMockedQuantity(int productId)
        {
            var mocked = new Mock<HttpMessageHandler>();
            mocked.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("15") });
            var httpClient = new HttpClient(mocked.Object);
            var response = await httpClient.GetAsync(string.Format("{0}/{1}", Configuration["MockedServiceBaseAddress"], Configuration["MockedServiceAction"]), CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStringAsync();
                return new Product() { Quantity = Convert.ToInt32(stream) };
            }

            return new Product() { Quantity = 0 };
        }


    }

}
