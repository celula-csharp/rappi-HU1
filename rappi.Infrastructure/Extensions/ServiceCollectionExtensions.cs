using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rappi.Infrastructure.Data;

namespace rappi.Infrastructure.Extensions;
// se crea funcion para hacer la conexion con la db inyectandola a program 
public static class ServiceCollectionExtensions
{
    //Registrar el Dbcontext

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 36))
                )
            );
        return services;
    }


}