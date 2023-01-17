using Microsoft.EntityFrameworkCore;
using ShoppingCart.DataAccessLayer.Db;
using ShoppingCart.DataAccessLayer.Interfaces;
using ShoppingCart.Entities.Generics;

namespace ShoppingCart.DataAccessLayer.Implementation
{
    public class Repository : IRepository
    {
        private GenericDbContext context;
        public Repository(IGenericContext context)
        {
            this.context = context as GenericDbContext;
        }

        public void Create<TEntity>(TEntity entity)
            where TEntity : class, TEntityType
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, TEntityType
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(int id)
            where TEntity : class, TEntityType
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, TEntityType
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                ThrowInvalidOperationException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (InvalidOperationException e)
            {
                ThrowInvalidOperationException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowInvalidOperationException(InvalidOperationException e)
        {
            Exception exception = e.InnerException != null ? e.InnerException : e;
            throw new InvalidOperationException(exception.Message, exception);
        }

        public virtual void Attach<TEntity>(TEntity entity)
             where TEntity : class, TEntityType
        {
            context.Set<TEntity>().Attach(entity);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void ChangeState<TEntity>(TEntity entity, State state)
             where TEntity : class, TEntityType
        {
            EntityState entityState = EntityState.Unchanged;
            switch (state)
            {
                case State.New:
                    entityState = EntityState.Added;
                    break;
                case State.Modified:
                    entityState = EntityState.Modified;
                    break;
                case State.Deleted:
                    entityState = EntityState.Deleted;
                    break;
                case State.ReadOnly:
                    entityState = EntityState.Unchanged;
                    break;
            }
            context.Entry(entity).State = entityState;
        }

        void IRepository.Detach<TEntity>(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }

        void IRepository.DeleteRange<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
    }
}
