using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Smartlockr.SignalRService
{
    public class Startup
    {
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddCors( options=> {
                options.AddPolicy("CORS_POLICY",
                    builder => builder.WithOrigins("https://localhost:44331", "https://localhost:5001")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                    
            }

            app.UseCors("CORS_POLICY");
            app.UseRouting();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<DomainUpdateHub>("/hub");
            });
        }
    }
}
