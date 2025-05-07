using Ecom.API.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Ecom.API.Extensions
{
    static public class ApiRegistration
    {
        public static IServiceCollection AddApiRegistration(this IServiceCollection services)
        {

            // Configure Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Configure IFileProvider
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            //ConfigureAPIBehaviorOptions (Catch ValidationError)
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray()
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            // Enable CORS
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("CorsPolicy", pot =>
            //    {
            //        pot.AllowAnyHeader()
            //        .AllowAnyMethod();
            //    });
            //});

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin() // ✅ Allow any domain
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            return services;
        }

    }
}