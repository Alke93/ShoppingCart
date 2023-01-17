using ShoppingCart.Entities.Generics;

namespace ShoppingCart.DataAccessLayer.Interfaces
{
    public interface IRepository : IDisposable
    {
        void Create<TEntity>(TEntity entity)
            where TEntity : class, TEntityType;
        void ChangeState<TEntity>(TEntity entity, State state)
            where TEntity : class, TEntityType;
        void Attach<TEntity>(TEntity entity) where TEntity : class, TEntityType;
        void Update<TEntity>(TEntity entity)
            where TEntity : class, TEntityType;

        void Delete<TEntity>(int id)
            where TEntity : class, TEntityType;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, TEntityType;

        void DeleteRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, TEntityType;

        void Detach<TEntity>(TEntity entity)
            where TEntity : class, TEntityType;

        void Save();

        Task SaveAsync();
    }
}
