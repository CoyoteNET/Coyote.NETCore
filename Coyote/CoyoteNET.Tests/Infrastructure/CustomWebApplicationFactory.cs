using CoyoteNET.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoyoteNET.Tests.Infrastructure
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<Context>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryHostDataBase");
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<Context>();
                db.Database.EnsureCreated();
            });
        }
    }
}