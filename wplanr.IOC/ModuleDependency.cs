using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using wplanr.DbContext.IDatabaseContext;
using wplanr.DbContext.MongoContext;
using wplanr.DTO.Interfaces;
using wplanr.Repository.Adapter;
using wplanr.Repository.Implementation;
using wplanr.Services.Implementation;

namespace wplanr.IOC
{
    public static class ModuleDependency
    {
        public static void RegisterModules(this IServiceCollection service)
        {
            var repositoryAssembly = Assembly.GetAssembly(typeof(AuthRepository));
            var serviceAssembly = Assembly.GetAssembly(typeof(AuthService));

            service.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly, serviceAssembly)
                    .Where(x => x.Name.EndsWith("Service") || x.Name.EndsWith("Repository") || x.Name.EndsWith("Provider") || x.Name.EndsWith("Adapter"))
                    .AsPublicImplementedInterfaces();

            service.AddTransient<IMongoAdapter, MongoAdapter>();
            service.AddTransient<IMongoContext, MongoContext>();
        }
    }
}
