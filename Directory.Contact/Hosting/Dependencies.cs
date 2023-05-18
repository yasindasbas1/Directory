using Directory.Contact.Services;
using Directory.Data;
using Microsoft.EntityFrameworkCore;

namespace Directory.Contact.Hosting
{
    public static class Dependencies
    {
        public static void ConfigureDependencies(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            services.AddDbContext<ContactContextDb>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("ConnectionString")!);
                builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                if (!env.IsDevelopment())
                    return;

                builder.EnableSensitiveDataLogging();
                builder.EnableDetailedErrors();

            });

            services.AddScoped<ContactService>();

        }
    }
    
}
