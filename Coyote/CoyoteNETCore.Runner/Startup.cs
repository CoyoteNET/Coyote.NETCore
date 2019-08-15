using CoyoteNETCore.Application.Account.Commands;
using CoyoteNETCore.Application.Services;
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
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using CoyoteNETCore.Application.Interfaces;
using CoyoteNETCore.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.AddMediatR(typeof(RegisterUserCommand).Assembly);

            services
                .AddMvc(o =>
                {
                    o.Filters.Add(typeof(ExceptionMiddleware));
                })
                .AddApplicationPart(typeof(HomeController).Assembly)
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining(typeof(RegisterUserCommandValidation)));

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<JwtService>();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Coyote API", Version = "v1" });
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coyote API v1");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
