using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using Xunit;
using System;
using System.Threading;
using CoyoteNETCore.Application.Threads.Commands;
using CoyoteNETCore.Shared.Entities;

namespace CoyoteNETCore.Tests
{
    public class ThreadTests : IDisposable
    {
        private readonly Context context;

        public ThreadTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
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
            var user = new User("User1", "User1@coyote.pub");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await context.AddAsync(threadCategory);
            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var command = new CreateThreadCommand("Body", "Title", threadCategory.Id, null, user.Id);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSucceeded);
            Assert.NotNull(await context.Threads.FirstOrDefaultAsync());
            Assert.NotNull(await context.ThreadCategories.FirstOrDefaultAsync());
            Assert.NotNull(await context.ForumSections.FirstOrDefaultAsync());
            Assert.True(await context.Posts.AnyAsync(x => x.Content == command.Body));
        }

        [Fact]
        public async Task Incorrectly_Created_Thread_1_CategoryDoesNotExist()
        {
            var handler = new CreateThreadCommand.Handler(context);
            var user = new User("User1", "User1@coyote.pub");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var command = new CreateThreadCommand("Body", "Title", threadCategory.Id, null, user.Id);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSucceeded);
            Assert.Null(await context.Threads.FirstOrDefaultAsync());
            Assert.False(await context.Posts.AnyAsync(x => x.Content == command.Body));
        }

        [Fact]
        public async Task Incorrectly_Created_Thread_2_UserDoesntExist()
        {
            var handler = new CreateThreadCommand.Handler(context);
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            var command = new CreateThreadCommand("Body", "Title", threadCategory.Id, "abc", 5);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(result.IsSucceeded);
            Assert.Null(await context.Threads.FirstOrDefaultAsync());
            Assert.False(await context.Posts.AnyAsync(x => x.Content == command.Body));
        }

        [Fact]
        public async Task Just_Created_Thread_Is_Obtainable()
        {
            var handler = new CreateThreadCommand.Handler(context);
            var user = new User("User1", "User1@coyote.pub");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await context.AddAsync(threadCategory);
                await context.AddAsync(user);
                await context.SaveChangesAsync();

            var payload = new CreateThreadCommand("Body", "Title", threadCategory.Id, null, user.Id);

            var result = await handler.Handle(payload, new CancellationToken());

            var handler2 = new GetThreadQuery.Handler(context);

            var result2 = await handler2.Handle(new GetThreadQuery(result.Value), new CancellationToken());

            Assert.True(result2.IsSucceeded);
            Assert.NotNull(result2.Value);
            Assert.Equal(payload.Title, result2.Value.Title);
            Assert.Contains(result2.Value.Posts, x => x.Content == payload.Body);
        }
    }
}
