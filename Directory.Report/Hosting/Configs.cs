using Microsoft.OpenApi.Models;

namespace Directory.Report.Hosting
{
    public static class Configs
    {
        public static void ConfigureCorsOrigin(this IApplicationBuilder app)
        {
            app.UseCors(options =>
            {
                options
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
            });
        }

        public static IMvcBuilder ConfigureModelValidationOptions(this IMvcBuilder builder)
        {
            return builder.ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
            })
                .AddMvcOptions(options =>
                {
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                });
        }


        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Directory.Report.Api.v1", new OpenApiInfo { Title = "Directory Api", Version = "v1.0" });

                var vScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. "
                                  + "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below."
                                  + "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", vScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {vScheme, new string[] { }}
                });
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsProduction())
                return;

            app.UseSwagger(options => { options.RouteTemplate = "docs/{documentName}/docs.json"; });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint("/docs/Directory.Report.Api.v1/docs.json", "Directory Api");
            });
        }
    }
}