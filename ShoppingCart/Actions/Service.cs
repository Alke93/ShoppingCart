using ShoppingCart.BusinessLogicLayer.Operations;
using ShoppingCart.DataAccessLayer.Db;
using ShoppingCart.DataAccessLayer.Interfaces;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Generics;
using ShoppingCart.Entities.Requests;
using ShoppingCart.Generics;

namespace ShoppingCart.Actions
{
    public class Service : IService
    {
        private GenericDbContext _context;
        private IReadOnlyRepository _readOnlyRepository;
        private IRepository _repository;
        private IConfiguration _configuration;

        public Service(IGenericContext context, IReadOnlyRepository readRepo, IRepository repo, IConfiguration configuration)
        {
            _context = context as GenericDbContext;
            _readOnlyRepository = readRepo;
            _repository = repo;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> Execute(IRequest item, SystemAction action)
        {
            if (item is null)
            {
                return await Task.FromResult(new ServiceResponse { Status = ResponseStatus.ModelNotValid, Description = "Request object cannot be null" });
            }

            try
            {
                GeneralSystemOperation operation = CreateSystemOperation(item, action);
                if (operation != null)
                {
                    ServiceResponse response;
                    if (action != SystemAction.Read)
                    {
                        await _context.Database.BeginTransactionAsync();
                        response = await operation.ExecuteOperation(item);
                        _context.Database.CommitTransaction();

                    }
                    else
                    {
                        response = await operation.ExecuteOperation(item);
                    }

                    return response;
                }

                return await Task.FromResult(new ServiceResponse { Status = ResponseStatus.OperationNotFound, Description = "Operation not found" });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ServiceResponse { Status = ResponseStatus.ServerError, Description = e.InnerException != null ? e.InnerException.Message : e.Message });
            }

        }

        private GeneralSystemOperation CreateSystemOperation(IRequest item, SystemAction action)
        {
            Type itemType = item.GetType();
            string typeName = GetOperationNamespace(itemType.Name, string.Concat(action.ToString(), item.GetMainTypeName()));
            Type type = Type.GetType(typeName, true);
            var operation = Activator.CreateInstance(type, new object[] { _readOnlyRepository, _repository, _configuration });
            return (GeneralSystemOperation)operation;
        }

        private string GetOperationNamespace(string typeName, string className)
        {
            return string.Concat(_configuration[ConfigParams.NAMESPACE], typeName, _configuration[ConfigParams.NAMESPACEEXTENSION], className, _configuration[ConfigParams.ASSEMBLY]);
        }
    }
}
