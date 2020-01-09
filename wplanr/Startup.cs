using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using wplanr.Core.ConfigurationModels;
using wplanr.DbContext.IDatabaseContext;
using wplanr.DbContext.MongoContext;
using wplanr.DTO.Interfaces;
using wplanr.IOC;
using wplanr.Modules;
using wplanr.Repository.Adapter;

namespace wplanr
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            //Configuration = configuration;
            _logger = logger;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _logger.LogInformation("Environemnt Name: " + env.EnvironmentName);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            // Configure jwt authentication
            var jwtSettings = Configuration.GetSection(nameof(JwtAuthenticationSettings)).Get<JwtAuthenticationSettings>();
            ApplicationModules.SetupJwtMechanism(services, jwtSettings);


            services.RegisterModules();


            services.Configure<MongoConnectionStrings>(Configuration.GetSection("MongoConnectionStrings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
