using Microsoft.Extensions.Configuration;
using ShoppingCart.DataAccessLayer.Interfaces;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Model;
using ShoppingCart.Entities.Requests;
using ShoppingCart.Entities.Responses;
using System.Text.RegularExpressions;

namespace ShoppingCart.BusinessLogicLayer.Operations.OrderRequestOperations
{
    internal class CreateOrder : GeneralSystemOperation
    {
        public CreateOrder() { }

        public CreateOrder(IReadOnlyRepository readOnlyRepo, IRepository repo, IConfiguration configuration) : base(readOnlyRepo, repo, configuration)
        {

        }

        protected override async Task<ServiceResponse> ConcreteOperation(object request)
        {
            var orderRequest = (OrderRequest)request;
            var shoppingCart = await ReadOnlyRepository.GetFirstAsync<Entities.Model.ShoppingCart>(item => item.CustomerId == orderRequest.CustomerId && !item.IsCompleted, null, "ShoppingItems,ShoppingItems.Product");
            if (shoppingCart == null)
            {
                return ResponseHelper.ItemNotFound("Shopping cart not found");
            }

            var discountSection = Configuration.GetSection("Discount");
            var hours = discountSection["Hours"];
            var amountsSection = discountSection.GetSection("Amount");
            double discountPercentage = 0;
            var startTime = new TimeSpan(Convert.ToInt32(hours.Split('-')[0]), 0, 0);
            var endTime = new TimeSpan(Convert.ToInt32(hours.Split('-')[1]), 0, 0);
            if (timeBetween(DateTime.Now, startTime, endTime))
            {
                var lastDigit = Convert.ToInt32(orderRequest.PhoneNumber.Last());
                if (lastDigit == 0)
                {
                    discountPercentage = Convert.ToDouble(amountsSection["Zero"]);
                }
                else if (lastDigit % 2 == 0)
                {
                    discountPercentage = Convert.ToDouble(amountsSection["EvenNumber"]);
                }
                else
                {
                    discountPercentage = Convert.ToDouble(amountsSection["OddNumber"]);
                }
            }

            var amount = calculateAmount(shoppingCart.ShoppingItems);
            var discount = calculateDiscount(amount, discountPercentage);

            var order = new Order()
            {
                Amount = amount,
                Discount = discount,
                Sum = Math.Round((amount - discount), 2),
                CustomerAddress = orderRequest.Address,
                CustomerPhoneNumber = orderRequest.PhoneNumber,
                ShoppingCartId = shoppingCart.Id
            };

            shoppingCart.IsCompleted = true;
            Repository.Update(shoppingCart);
            Repository.Create(order);
            shoppingCart.ShoppingItems.ForEach(item =>
            {
                item.Product.ReservedQuantity -= item.Quantity;
                item.Product.Quantity -= item.Quantity;
                item.Product.Quantity = item.Product.Quantity < 0 ? 0 : item.Product.Quantity;
                Repository.Update(item.Product);
            });

            await Repository.SaveAsync();

            var response = new OrderResponse()
            {
                Amount = order.Amount,
                Sum = order.Sum,
                CustomerAddress = orderRequest.Address,
                CustomerId = orderRequest.CustomerId,
                CustomerPhoneNumber = orderRequest.PhoneNumber,
                OrderId = order.Id,
                Discount = order.Discount
            };

            return ResponseHelper.SuccessfulResponse(response, "Order created");
        }

        protected override async Task<bool> Validate(object request)
        {
            var orderRequest = (OrderRequest)request;
            string pattern = @"\([+]?([0-9]{3})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})";
            var regex = new Regex(pattern);
            return await Task.Run(() => orderRequest.CustomerId > 0 && !string.IsNullOrWhiteSpace(orderRequest.Address)
                && !string.IsNullOrWhiteSpace(orderRequest.PhoneNumber) && regex.IsMatch(orderRequest.PhoneNumber));
        }

        private bool timeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            TimeSpan now = datetime.TimeOfDay;
            if (start < end)
            {
                return start <= now && now <= end;
            }

            return !(end < now && now < start);
        }

        private double calculateDiscount(double amount, double discountPercentage)
        {
            return Math.Round(discountPercentage * amount, 2);
        }

        private double calculateAmount(List<ShoppingItem> shoppingItems)
        {
            return Math.Round(shoppingItems.Sum(item => item.TotalPrice), 2);
        }
    }
}
