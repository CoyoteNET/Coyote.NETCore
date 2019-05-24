using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoyoteNETCore.Controllers;
using CoyoteNETCore.DAL;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Swashbuckle.AspNetCore.SwaggerUI;
using MediatR;
using CoyoteNETCore.Application.Threads.Command;

namespace Coyote.NETCore
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
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")), ServiceLifetime.Transient);

            services.AddMediatR(typeof(CreateThreadCommand).Assembly);

            services
                .AddMvc()
                .AddApplicationPart(typeof(HomeController).Assembly)
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Coyote API", Version = "v1" });
            });

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/LogIn";
                    options.LogoutPath = "/Account/LogOff";
                })
                .AddGitHub(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Context context)
        {
            // in order to setup this project easier
           context.Database.EnsureCreated();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coyote API V1");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });

            //app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
