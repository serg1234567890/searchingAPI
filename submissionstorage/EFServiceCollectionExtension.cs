using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using submissionstorage.Stories.Common;
using System;

namespace submissionstorage
{
    public static class EFServiceCollectionExtension
    {
        public static IServiceCollection AddEntityFrameworkContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where TContext : DbContext
        {
            services.AddDbContext<TContext>(optionsAction, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddEntityFrameworkStoreService<TEntity, TStoreService>(this IServiceCollection services) where TStoreService : class, IEFStore<TEntity> where TEntity : class
        {
            services.AddScoped<TStoreService>();

            return services;
        }
    }
}
