using Application.Bikes;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, 
            IConfiguration configuration) {

                services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                    //policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            services.AddSingleton<IConfiguration>(configuration);
            //services.AddScoped<IRepository, EfRepository.Bikes>();
            services.AddScoped<IRepository, SqlRepository.Bikes>();
            
            services.AddMediatR(typeof(List.Handler).Assembly);

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;

        }
    }
}