
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
            {
                services.AddDbContext<DataContext>(opt => 
                    {
                        opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
                    });

                    //authentication header in
                    services.AddCors();
                    services.AddScoped<ITokenService, TokenService>(); 
                    //Dispose the service once end point of the controller is reached, no need this service any further

                    return services;
            }
    }
}