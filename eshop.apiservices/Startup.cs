using AutoMapper;
using eshop.apiservices.Cache;
using eshop.apiservices.Repositories;
using eshop.apiservices.Services;
using eshop.core.ImageSetting;
using eshop.core.Interfaces.Repositories;
using eshop.core.Interfaces.Services;
using eshop.core.JwtSettings;
using eshop.core.MapperProfile;
using eshop.infrastructure.JwtAuth;
using eshop.infrastructure.RedisCache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;

namespace eshop.apiservices
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                        {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                    }
                });
            });

            services.AddControllers();
            services.AddSingleton(Configuration);

            //services.AddRedisCache(Configuration);
            //services.AddKafkaLog(Configuration);

            var jwtTokenConfig = Configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();
            services.AddJwtAuth(jwtTokenConfig);

            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IManagerAuthService, ManagerAuthService>();
            services.AddScoped<ICustomerAuthService, CustomerAuthService>();

            services.AddScoped<ICategoryCached, CategoryCached>();
            services.AddScoped<IManagerCached, ManagerCached>();
            services.AddScoped<IProductCached, ProductCached>();
            services.AddScoped<ICustomerCached, CustomerCached>();

            services.AddAutoMapper(typeof(MapperProfile));

            var imgSetting = Configuration.GetSection("ImageSetting").Get<ImageSetting>();
            services.AddSingleton(imgSetting);
            services.AddScoped<IFileService, FileService>();

            services.AddRedisCache(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eshop API");
            });
            //app.UseMiddleware<KafkaLogMidleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
