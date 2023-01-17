using Microsoft.Extensions.Configuration;
using ShoppingCart.BusinessLogicLayer.Helpers;
using ShoppingCart.DataAccessLayer.Interfaces;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Generics;
using ShoppingCart.Entities.Requests;

namespace ShoppingCart.BusinessLogicLayer.Operations
{
    public interface IOperation: IDisposable { Task<ServiceResponse> ExecuteOperation(object request); }
    public abstract class GeneralSystemOperation : IOperation
    {
        public GeneralSystemOperation()
        {

        }
        public GeneralSystemOperation(IReadOnlyRepository readOnlyRepo, IRepository repo, IConfiguration configuration)
        {
            ReadOnlyRepository = readOnlyRepo;
            Repository = repo;
            Configuration = configuration;
        }

        protected IRepository Repository { get; }

        protected IReadOnlyRepository ReadOnlyRepository { get; }

        protected IConfiguration Configuration { get;}

        protected abstract Task<ServiceResponse> ConcreteOperation(object request); 

        protected abstract Task<bool> Validate(object request);


        protected ResponseHelper ResponseHelper
        {
            get
            {
                return ResponseHelper.Instance;
            }
        }

        public async Task<ServiceResponse> ExecuteOperation(object request) 
        {
            try
            {
                var isValid = await Validate(request);
                if (isValid)
                {
                    return await ConcreteOperation(request);
                }

                return await NotValidEntity();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<ServiceResponse> NotValidEntity()
        {
            return await Task.FromResult(ResponseHelper.ModelNotValid("Entity is null or of invalid type"));
        }

        public void Dispose() { }

    }
}
