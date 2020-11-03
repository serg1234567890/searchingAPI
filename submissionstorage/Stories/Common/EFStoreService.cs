using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace submissionstorage.Stories.Common
{
    public abstract class EFStoreService<TEntity, TContext> : IEFStore<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly TContext context;

        public EFStoreService(TContext context)
        {
            this.context = context;
        }

        protected IQueryable<TEntity> Query
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        public virtual void Insert(TEntity entity)
        {
            context.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            context.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            context.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            context.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            context.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            context.RemoveRange(entities);
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }

        public virtual void Save(bool acceptAllChangesOnSuccess)
        {
            context.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
