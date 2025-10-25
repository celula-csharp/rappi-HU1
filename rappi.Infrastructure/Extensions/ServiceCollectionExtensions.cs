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
        string conn = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                conn,
                new MySqlServerVersion(conn)
                )
            );
        return services;
    }


}