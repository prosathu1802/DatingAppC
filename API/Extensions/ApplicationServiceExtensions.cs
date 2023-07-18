
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;
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
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    //Dispose the service once end point of the controller is reached, no need this service any further
                    services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
                    services.AddScoped<IPhotoService, PhotoService>();//Destroy when HttpRequest ends
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<LogUserActivity>();
                    services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
                    services.AddSignalR();
                    services.AddSingleton<PresenceTracker>();   //This service needs to live as long as application is running


                    return services;
            }
    }
}