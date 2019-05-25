using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using Xunit;
using CoyoteNETCore.Shared;
using System;
using CoyoteNETCore.Application.Threads.Command;
using System.Threading;
using System.Reflection;

namespace CoyoteNETCore.Tests
{
    public class ThreadTests : IDisposable
    {
        private readonly Context context;

        public ThreadTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseInMemoryDatabase("database");
            context = new Context(optionsBuilder.Options);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Correctly_Created_Thread_1()
        {
            var handler = new CreateThreadCommand.Handler(context);
            var user = new User("User1", "User1@coyote.pub", "#$%^$^*^#%$#^&%7876976234523634", "235435745134[]cvxvcb/[;");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await context.AddAsync(threadCategory);
            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var command = new CreateThreadCommand("Body", "Title", threadCategory.Id, user.Id);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            Assert.NotNull(await context.Threads.FirstOrDefaultAsync());
            Assert.NotNull(await context.ThreadCategories.FirstOrDefaultAsync());
            Assert.NotNull(await context.ForumSections.FirstOrDefaultAsync());
        }

        [Fact]
        public async Task Incorrectly_Created_Thread_1_CategoryDoesNotExist()
        {
            var handler = new CreateThreadCommand.Handler(context);
            var user = new User("User1", "User1@coyote.pub", "#$%^$^*^#%$#^&%7876976234523634", "235435745134[]cvxvcb/[;");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var command = new CreateThreadCommand("Body", "Title", threadCategory.Id, user.Id);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.Success);
            Assert.Null(await context.Threads.FirstOrDefaultAsync());
        }

        [Fact]
        public async Task Incorrectly_Created_Thread_2_UserDoesntExist()
        {
            var handler = new CreateThreadCommand.Handler(context);
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            var command = new CreateThreadCommand("Body", "Title", threadCategory.Id, 5);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.Success);
            Assert.Null(await context.Threads.FirstOrDefaultAsync());
        }
    }
}
