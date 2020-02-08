using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CoyoteNET.DAL;
using Xunit;
using System;
using System.Linq;
using CoyoteNET.Shared.Entities;

namespace CoyoteNET.Tests
{
    public class DatabaseModelVerification : IDisposable
    {
        private readonly Context _context;

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        public DatabaseModelVerification()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseInMemoryDatabase("InMemoryDbForTesting");
            _context = new Context(optionsBuilder.Options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task CreateUser_With_Avatar()
        {
            Assert.Empty(await _context.Users.ToListAsync());
            await _context.Users.AddAsync(new User("test", "test")
            {
                
            });

            await _context.SaveChangesAsync();
            await _context.Files.AddAsync(new File("asd.jpg", "asd", await _context.Users.FirstAsync()));
            await _context.SaveChangesAsync();

            var user = await _context.Users.Include(x => x.Avatar).FirstAsync();

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

            _context.Threads.Add(thread1);
            _context.Threads.Add(thread2);
            await _context.SaveChangesAsync();

            Assert.Equal(1, await _context.Users.CountAsync());
            Assert.Equal("Tests", _context.ForumSections.FirstOrDefault().Name);
            Assert.Equal("Test #1", _context.ThreadCategories.FirstOrDefault().Name);
            Assert.Equal(2, await _context.Threads.CountAsync());
            Assert.True(_context.Threads.FirstOrDefault(x => x.Title == "Sample Title").Subscribers.Any());
            Assert.True(_context.Threads.FirstOrDefault(x => x.Title == "Sample Title 2").Subscribers.Any());
            Assert.True(_context.Threads.FirstOrDefault(x => x.Title == "Sample Title").Posts.Count() == 2);
            Assert.Equal(2, _context.Posts.Include(x => x.Subscribers).Where(x => x.Subscribers.Any(z => z.Subscriber.Username == "a")).Count());
        }

        [Fact]
        public async Task Relation_Between_File_User_UserFile()
        {
            await _context.Files.AddAsync(new File("asd.jpg", "asd", new User("test", "test"){}));
            await _context.SaveChangesAsync();

            var user = _context.Users.First();
            user.DownloadedFilesLog.Add(new UserFile(user, _context.Files.First()));

            await _context.SaveChangesAsync();

            Assert.True(user.DownloadedFilesLog.Count == 1);

            _context.DownloadFileLog.Remove(_context.DownloadFileLog.First());

            await _context.SaveChangesAsync();

            Assert.True(_context.Files.Count() == 1);
            Assert.True(_context.Users.Count() == 1);
            Assert.True(_context.Users.First().DownloadedFilesLog.Count == 0);
            Assert.True(_context.DownloadFileLog.Count() == 0);
        }
    }
}
