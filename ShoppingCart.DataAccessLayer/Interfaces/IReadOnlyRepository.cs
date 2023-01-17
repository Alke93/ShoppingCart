using ShoppingCart.Entities.Generics;
using System.Linq.Expressions;

namespace ShoppingCart.DataAccessLayer.Interfaces
{
    public interface IReadOnlyRepository : IDisposable
    {
        IEnumerable<TEntity> GetAll<TEntity>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        string includeProperties,
        int? skip = null,
        int? take = null)
        where TEntity : class, TEntityType;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            int? skip = null,
            int? take = null)
            where TEntity : class, TEntityType;

        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            int? skip = null,
            int? take = null)
            where TEntity : class, TEntityType;

        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            int? skip = null,
            int? take = null)
            where TEntity : class, TEntityType;

        TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter ,
            string includeProperties)
            where TEntity : class, TEntityType;

        Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties)
            where TEntity : class, TEntityType;

        TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties)
            where TEntity : class, TEntityType;

        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter ,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null ,
            string includeProperties = "")
            where TEntity : class, TEntityType;

        TEntity GetById<TEntity>(int id)
            where TEntity : class, TEntityType;

        ValueTask<TEntity> GetByIdAsync<TEntity>(int id)
            where TEntity : class, TEntityType;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, TEntityType;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter )
            where TEntity : class, TEntityType;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, TEntityType;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, TEntityType;
    }
}
