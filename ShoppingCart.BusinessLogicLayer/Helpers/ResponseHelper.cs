using ShoppingCart.Entities;

namespace ShoppingCart.BusinessLogicLayer.Helpers
{
    public sealed class ResponseHelper
    {
        private static ResponseHelper? _instance;
        private static readonly object padlock = new object();

        private ResponseHelper() { }

        public static ResponseHelper Instance
        {
            get
            {
                if (_instance is null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ResponseHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        public ServiceResponse SuccessfulResponse(object body = null, string description = "")
        {
            return new ServiceResponse { Status = ResponseStatus.Ok, Body = body, Description = description };
        }

        public ServiceResponse ItemNotFound(string description = "")
        {
            return new ServiceResponse { Status = ResponseStatus.ItemNotFound, Description = string.IsNullOrWhiteSpace(description) ? "Item not found or empty" : description };
        }

        public ServiceResponse ModelNotValid(string description)
        {
            return new ServiceResponse { Status = ResponseStatus.ModelNotValid, Description = description };
        }

        public ServiceResponse ServerError(string description)
        {
            return new ServiceResponse { Status = ResponseStatus.ServerError, Description = description };
        }
    }
}
