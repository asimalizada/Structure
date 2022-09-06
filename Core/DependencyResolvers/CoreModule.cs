using Core.Business.Abstract;
using Core.Business.Concrete;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DataAccess.Abstract;
using Core.DataAccess.Concrete.EntityFramework;
using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
            serviceCollection.AddSingleton<ITokenHelper, JwtHelper>();
            serviceCollection.AddSingleton<IAuthService, AuthManager>();
            serviceCollection.AddSingleton<IUserService, UserManager>();
            serviceCollection.AddSingleton<IUserDal, EfUserDal>();
        }
    }
}
