using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS1
{
    public class Startup
    {
        private readonly ILogger logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // Hello
            this.Configuration.Bind("Hello");
            this.logger = default(ILogger);
            this.logger = this.GetEarlyInitializationLogger();
        }

        private ILogger GetEarlyInitializationLogger() 
        {
            using var loggerFactory = LoggerFactory.Create(this.LoggingBuilder());
            return loggerFactory.CreateLogger("Initialization");
        }

        private Action<ILoggingBuilder> LoggingBuilder()
        {
            return loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddConfiguration(this.Configuration.GetSection("Logging"));
                loggingBuilder.AddDebug();
            };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
