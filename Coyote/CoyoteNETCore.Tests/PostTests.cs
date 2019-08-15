using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using Xunit;
using CoyoteNETCore.Shared;
using System;
using System.Threading;
using CoyoteNETCore.Application.Threads.Commands;
using Coyote.NETCore;
using System.Net.Http;
using System.Linq;
using CoyoteNETCore.Shared.Entities;

namespace CoyoteNETCore.Tests
{
    public class PostTests : IDisposable, IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly Context context;

        public PostTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new Context(optionsBuilder.Options);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task Correctly_Created_Thread_And_Then_New_Post_Written()
        {
            var thread_handler = new CreateThreadCommand.Handler(context);
            var user = new User("User1", "User1@coyote.pub");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await context.AddAsync(threadCategory);
            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var thread_command = new CreateThreadCommand("Body", "Title", threadCategory.Id, null, user.Id);
            var thread_result = await thread_handler.Handle(thread_command, new CancellationToken());

            Assert.True(thread_result.IsSucceeded);
            Assert.NotNull(await context.Threads.FirstOrDefaultAsync());
            Assert.NotNull(await context.ThreadCategories.FirstOrDefaultAsync());
            Assert.NotNull(await context.ForumSections.FirstOrDefaultAsync());
            Assert.True(await context.Posts.AnyAsync(x => x.Content == thread_command.Body));

            var post_handler = new WritePostCommand.Handler(context);
            var post_command = new WritePostCommand(thread_result.Value, "test body", user.Id);
            var post_result = await post_handler.Handle(post_command, new CancellationToken());

            Assert.True(post_result.IsSucceeded);

            var getThread_handler = new GetThreadQuery.Handler(context);

            var get_thread_with_posts = await getThread_handler.Handle(new GetThreadQuery(thread_result.Value), new CancellationToken());

            Assert.True(get_thread_with_posts.IsSucceeded);
            Assert.Equal(2, get_thread_with_posts.Value.Posts.Count);
        }

        [Fact]
        public async Task Correctly_Created_Thread_And_Then_New_Incorrect_Post_Cannot_Be_Written()
        {
            var thread_handler = new CreateThreadCommand.Handler(context);
            var user = new User("User1", "User1@coyote.pub");
            var threadCategory = new ThreadCategory("test", "test", new ForumSection("test section"));

            await context.AddAsync(threadCategory);
            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var thread_command = new CreateThreadCommand("Body", "Title", threadCategory.Id, "asd", user.Id);
            var thread_result = await thread_handler.Handle(thread_command, new CancellationToken());

            Assert.True(thread_result.IsSucceeded);
            Assert.NotNull(await context.Threads.FirstOrDefaultAsync());
            Assert.NotNull(await context.ThreadCategories.FirstOrDefaultAsync());
            Assert.NotNull(await context.ForumSections.FirstOrDefaultAsync());
            Assert.True(await context.Posts.AnyAsync(x => x.Content == thread_command.Body));

            var post_handler = new WritePostCommand.Handler(context);
            var post_command = new WritePostCommand(245, "", user.Id);

            var post_result = await post_handler.Handle(post_command, new CancellationToken());

            Assert.False(post_result.IsSucceeded);

            var getThread_handler = new GetThreadQuery.Handler(context);

            var get_thread_with_posts = await getThread_handler.Handle(new GetThreadQuery(thread_result.Value), new CancellationToken());

            Assert.True(get_thread_with_posts.IsSucceeded);
            Assert.Equal(1, get_thread_with_posts.Value.Posts.Count);
        }
    }
}
