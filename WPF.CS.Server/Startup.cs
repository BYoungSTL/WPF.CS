using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using WPF.CS.Application.Interfaces;
using WPF.CS.Application.Services;
using WPF.CS.Data.Configs;
using WPF.CS.Data.CRUDs;
using WPF.CS.Data.Entities;
using WPF.CS.Data.Interfaces;
using WPF.CS.Data.Mappers;

namespace WPF.CS.Server
{
    public class Startup(IConfiguration configuration)
    {
        //private const string DatabaseConfigName = "Database";

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("_origins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173",
                            "https://localhost:7107").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            //ConfigureDatabase(services);
            AddServices(services);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors("_origins");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IImageAppService, ImageAppService>();
            services.AddScoped<IImageService, ImageService>();
        }

        //private void ConfigureDatabase(IServiceCollection services)
        //{
        //    ConfigureEntityMapping();

        //    var secOpts = Configuration
        //        .GetSection(DatabaseConfigName)
        //        .Get<DatabaseConfig>();

        //    services.AddSingleton(new MongoClient(secOpts!.ConnectionString).GetDatabase(secOpts.Name));
        //}

        //private static void ConfigureEntityMapping()
        //{
        //    Mapper<Image>.EntityMapper();
        //}
    }
}
