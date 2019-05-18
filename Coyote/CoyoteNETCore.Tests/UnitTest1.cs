using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using Xunit;
using CoyoteNETCore.Shared;
using System;

namespace CoyoteNETCore.Tests
{
    public class UnitTest1
    {
        private readonly Context c;

        public UnitTest1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseInMemoryDatabase("database");
            c = new Context(optionsBuilder.Options);
        }

        [Fact]
        public async Task Test1()
        {
            Assert.Empty(await c.Users.ToListAsync());
            await c.Users.AddAsync(new User("test", "test")
            {
                
            });

            await c.SaveChangesAsync();
            await c.Files.AddAsync(new File("asd.jpg", "asd", await c.Users.FirstAsync()));
            await c.SaveChangesAsync();

            var user = await c.Users.Include(x => x.Avatar).FirstAsync();

            Assert.Equal("asd.jpg", user.Avatar.UserFileName);
        }
    }
}
