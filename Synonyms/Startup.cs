using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synonyms.Models.Options;
using Synonyms.Services;

namespace Synonyms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddTransient<ISynonymService, SynonymService>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

#if DEBUG
            app.UseCors(x =>
                {
                    x.AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowAnyOrigin()
                     .Build();
                });
#endif

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Request.Path == "/")
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/");
                }
            });

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
