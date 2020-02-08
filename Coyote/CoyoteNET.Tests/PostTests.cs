using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNET.DAL;
using Xunit;
using System;
using System.Threading;
using CoyoteNET.Application.Threads.Commands;
using CoyoteNET.Shared.Entities;

namespace CoyoteNET.Tests
{
    public class PostTests : IDisposable
    {
        private readonly Context _context;

        public PostTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new Context(optionsBuilder.Options);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Correctly_Created_Thread_And_Then_New_Post_Written()
        {
            var thread_handler = new CreateThreadCommand.Handler(_context);
            var user = new User("User1", "User1@coyote.pub");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await _context.AddAsync(threadCategory);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var thread_command = new CreateThreadCommand("Body", "Title", threadCategory.Id, null, user.Id);
            var thread_result = await thread_handler.Handle(thread_command, new CancellationToken());

            Assert.True(thread_result.IsSucceeded);
            Assert.NotNull(await _context.Threads.FirstOrDefaultAsync());
            Assert.NotNull(await _context.ThreadCategories.FirstOrDefaultAsync());
            Assert.NotNull(await _context.ForumSections.FirstOrDefaultAsync());
            Assert.True(await _context.Posts.AnyAsync(x => x.Content == thread_command.Body));

            var post_handler = new WritePostCommand.Handler(_context);
            var post_command = new WritePostCommand(thread_result.Value, "test body", user.Id);
            var post_result = await post_handler.Handle(post_command, new CancellationToken());

            Assert.True(post_result.IsSucceeded);

            var getThread_handler = new GetThreadQuery.Handler(_context);

            var get_thread_with_posts = await getThread_handler.Handle(new GetThreadQuery(thread_result.Value), new CancellationToken());

            Assert.True(get_thread_with_posts.IsSucceeded);
            Assert.Equal(2, get_thread_with_posts.Value.Posts.Count);
        }

        [Fact]
        public async Task Correctly_Created_Thread_And_Then_New_Incorrect_Post_Cannot_Be_Written()
        {
            var thread_handler = new CreateThreadCommand.Handler(_context);
            var user = new User("User1", "User1@coyote.pub");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await _context.AddAsync(threadCategory);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var thread_command = new CreateThreadCommand("Body", "Title", threadCategory.Id, "asd", user.Id);
            var thread_result = await thread_handler.Handle(thread_command, new CancellationToken());

            Assert.True(thread_result.IsSucceeded);
            Assert.NotNull(await _context.Threads.FirstOrDefaultAsync());
            Assert.NotNull(await _context.ThreadCategories.FirstOrDefaultAsync());
            Assert.NotNull(await _context.ForumSections.FirstOrDefaultAsync());
            Assert.True(await _context.Posts.AnyAsync(x => x.Content == thread_command.Body));

            var post_handler = new WritePostCommand.Handler(_context);
            var post_command = new WritePostCommand(245, "", user.Id);

            var post_result = await post_handler.Handle(post_command, new CancellationToken());

            Assert.False(post_result.IsSucceeded);

            var getThread_handler = new GetThreadQuery.Handler(_context);

            var get_thread_with_posts = await getThread_handler.Handle(new GetThreadQuery(thread_result.Value), new CancellationToken());

            Assert.True(get_thread_with_posts.IsSucceeded);
            Assert.Equal(1, get_thread_with_posts.Value.Posts.Count);
        }
    }
}
