using System;
using CoyoteNET.Application.Account.Commands;
using CoyoteNET.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoyoteNET.Controllers;
using CoyoteNET.DAL;
using Swashbuckle.AspNetCore.SwaggerUI;
using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using CoyoteNET.Application.Interfaces;
using CoyoteNET.Shared.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace CoyoteNET
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
            var connectionStringConfiguration = Environment.GetEnvironmentVariable("ConnectionStringConfiguration") ?? "Default";
            var connectionString = Configuration.GetConnectionString(connectionStringConfiguration);
            Retry.Do(() => CheckDataBaseConnection(connectionString), TimeSpan.FromSeconds(15), 5);
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

            services.AddMediatR(typeof(RegisterUserCommand).Assembly);
            services
                .AddControllers(o =>
                {
                    o.Filters.Add(typeof(ExceptionMiddleware));
                })
                .AddNewtonsoftJson()
                .AddApplicationPart(typeof(HomeController).Assembly)
                .AddControllersAsServices()
                .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining(typeof(RegisterInputValidation)));

            services.AddHealthChecks();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<JwtService>();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coyote API", Version = "v1" });
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context context)
        {
            // in order to setup this project easier
            context.Database.EnsureCreated();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coyote API v1");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
        private static void CheckDataBaseConnection(string connectionString)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            conn.Close();
        }
    }
}
