using System.Collections.Generic;

namespace submissionstorage.Stories.Common
{
    public interface IEFStore<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        void Save();
        void Save(bool acceptAllChangesOnSuccess);
    }
}
