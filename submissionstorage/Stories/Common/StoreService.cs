using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace submissionstorage.Stories.Common
{
    public abstract class StoreService<TEntity, TContext> : EFStoreService<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        public StoreService(TContext context) : base(context) { }
    }
}
