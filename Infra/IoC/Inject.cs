using Domain.Interface.Repository;
using Domain.Interface.Service;
using Infra.Repository;
using Infra.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public static class Inject
    {
        public static void InjectService(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            #endregion
        }
    }
}
