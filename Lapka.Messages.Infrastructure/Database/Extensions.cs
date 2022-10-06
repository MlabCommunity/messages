using Convey;
using Lapka.Messages.Core.Repositories;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Lapka.Messages.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lapka.Messages.Infrastructure.Database;

public static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAppUserRepository,AppUserRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        
        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<AppDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddScoped<AppDbContext>();

        return services;
    }
}