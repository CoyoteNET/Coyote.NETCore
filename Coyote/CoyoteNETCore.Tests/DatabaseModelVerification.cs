using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using Xunit;
using CoyoteNETCore.Shared;
using System;
using System.Linq;


namespace CoyoteNETCore.Tests
{
    public class DatabaseModelVerification : IDisposable
    {
        private readonly Context c;

        public void Dispose()
        {
            c.Database.EnsureDeleted();
        }

        public DatabaseModelVerification()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CoyoteNET_Tests;Trusted_Connection=True;");
            c = new Context(optionsBuilder.Options);
            c.Database.EnsureCreated();
        }

        [Fact]
        public async Task CreatingUserWithAvatar()
        {
            await c.Users.AddAsync(new User("test", "test", "test", "test")
            {
                
            });

            await c.SaveChangesAsync();
            await c.Files.AddAsync(new File("asd.jpg", "asd", await c.Users.FirstAsync()));
            await c.SaveChangesAsync();

            var user = await c.Users.Include(x => x.Avatar).FirstAsync();

            Assert.Equal("asd.jpg", user.Avatar.UserFileName);
        }

        [Fact]
        public async Task Database_subscription_test()
        {
            var user = new User("a", "b", "c", "d");
            var section = new ForumSection("Tests");
            var category = new ThreadCategory("Test #1", "Test 1", section);
            var thread1 = new Thread(category, "a,b,c,d,e", "Sample Title");
            var thread2 = new Thread(category, "a,b,c,d,e", "Sample Title 2");

            thread1.Subscribers.Add(new SubscriptionThread(user));
            thread2.Subscribers.Add(new SubscriptionThread(user));

            var post1 = new Post("1", thread1, user);
            var post2 = new Post("2", thread1, user);

            thread1.Posts.Add(post1);
            thread1.Posts.Add(post2);

            post1.Subscribers.Add(new SubscriptionPost(user));
            post2.Subscribers.Add(new SubscriptionPost(user));

            c.Threads.Add(thread1);
            c.Threads.Add(thread2);
            await c.SaveChangesAsync();

            Assert.Equal(1, await c.Users.CountAsync());
            Assert.Equal("Tests", c.ForumSections.FirstOrDefault().Name);
            Assert.Equal("Test #1", c.ThreadCategories.FirstOrDefault().Name);
            Assert.Equal(2, await c.Threads.CountAsync());
            Assert.True(c.Threads.FirstOrDefault(x => x.Title == "Sample Title").Subscribers.Any());
            Assert.True(c.Threads.FirstOrDefault(x => x.Title == "Sample Title 2").Subscribers.Any());
            Assert.True(c.Threads.FirstOrDefault(x => x.Title == "Sample Title").Posts.Count() == 2);
            Assert.Equal(2, c.Posts.Include(x => x.Subscribers).Where(x => x.Subscribers.Any(z => z.Subscriber.Name == "a")).Count());
        }

    }
}
