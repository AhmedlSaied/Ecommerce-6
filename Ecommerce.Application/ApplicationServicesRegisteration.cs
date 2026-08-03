using Ecommerce.Application.contracts;
using Ecommerce.Application.Profiles;
using Ecommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Ecommerce.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddAutoMapper(option => option.AddProfile(new ProductProfile()),typeof(ApplicationServicesRegisteration).Assembly);
            services.AddAutoMapper(option => option.AddProfile(new CustomerBasketProfile()),typeof(ApplicationServicesRegisteration).Assembly);
            services.AddAutoMapper(option => option.AddProfile(new OrderProfile()),typeof(ApplicationServicesRegisteration).Assembly);
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IBasketItemService, BasketItemService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<ICachedDataService, CachedDataService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
