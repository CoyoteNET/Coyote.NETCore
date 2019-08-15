using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using Xunit;
using CoyoteNETCore.Shared;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using CoyoteNETCore.Shared.Entities;

namespace CoyoteNETCore.Tests
{
    public class DatabaseModelVerification : IDisposable
    {
        private Context context;

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        public DatabaseModelVerification()
        {
            var config = new ConfigurationBuilder()
                             .AddJsonFile("appsettings.json")
                             .Build();

            try
            {
                SetupRealDb(config.GetConnectionString("AppVeyor"));
            }
            catch
            {
                SetupRealDb(config.GetConnectionString("TestDb"));
            }
        }

        #pragma warning disable xUnit1013 // Public method should be marked as test
        public void SetupRealDb(string conn)
        {
            Console.WriteLine($"Using CoyoteNET_TestDatabase {conn}");
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(conn);
            context = new Context(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Fact]
        public async Task CreateUser_With_Avatar()
        {
            Assert.Empty(await context.Users.ToListAsync());
            await context.Users.AddAsync(new User("test", "test")
            {
                
            });

            await context.SaveChangesAsync();
            await context.Files.AddAsync(new File("asd.jpg", "asd", await context.Users.FirstAsync()));
            await context.SaveChangesAsync();

            var user = await context.Users.Include(x => x.Avatar).FirstAsync();

            Assert.Equal("asd.jpg", user.Avatar.UserFileName);
        }

        [Fact]
        public async Task Database_subscription_test()
        {
            var user = new User("a", "b");
            var section = new ForumSection("Tests");
            var category = new ThreadCategory("Test #1", "Test 1", section);
            var thread1 = new Thread(category, "a,b,c,d,e", "Sample Title", user);
            var thread2 = new Thread(category, "a,b,c,d,e", "Sample Title 2", user);

            thread1.Subscribers.Add(new SubscriptionThread(user));
            thread2.Subscribers.Add(new SubscriptionThread(user));

            var post1 = new Post("1", thread1, user);
            var post2 = new Post("2", thread1, user);

            thread1.Posts.Add(post1);
            thread1.Posts.Add(post2);

            post1.Subscribers.Add(new SubscriptionPost(user));
            post2.Subscribers.Add(new SubscriptionPost(user));

            context.Threads.Add(thread1);
            context.Threads.Add(thread2);
            await context.SaveChangesAsync();

            Assert.Equal(1, await context.Users.CountAsync());
            Assert.Equal("Tests", context.ForumSections.FirstOrDefault().Name);
            Assert.Equal("Test #1", context.ThreadCategories.FirstOrDefault().Name);
            Assert.Equal(2, await context.Threads.CountAsync());
            Assert.True(context.Threads.FirstOrDefault(x => x.Title == "Sample Title").Subscribers.Any());
            Assert.True(context.Threads.FirstOrDefault(x => x.Title == "Sample Title 2").Subscribers.Any());
            Assert.True(context.Threads.FirstOrDefault(x => x.Title == "Sample Title").Posts.Count() == 2);
            Assert.Equal(2, context.Posts.Include(x => x.Subscribers).Where(x => x.Subscribers.Any(z => z.Subscriber.Username == "a")).Count());
        }

        [Fact]
        public async Task Relation_Between_File_User_UserFile()
        {
            await context.Files.AddAsync(new File("asd.jpg", "asd", new User("test", "test"){}));
            await context.SaveChangesAsync();

            var user = context.Users.First();
            user.DownloadedFilesLog.Add(new UserFile(user, context.Files.First()));

            await context.SaveChangesAsync();

            Assert.True(user.DownloadedFilesLog.Count == 1);

            context.DownloadFileLog.Remove(context.DownloadFileLog.First());

            await context.SaveChangesAsync();

            Assert.True(context.Files.Count() == 1);
            Assert.True(context.Users.Count() == 1);
            Assert.True(context.Users.First().DownloadedFilesLog.Count == 0);
            Assert.True(context.DownloadFileLog.Count() == 0);
        }
    }
}
