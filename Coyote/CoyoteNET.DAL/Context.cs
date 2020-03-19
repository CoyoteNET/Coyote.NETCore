using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CoyoteNET.Shared.Entities;

namespace CoyoteNET.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Thread> Threads { get; set; }

        public DbSet<ThreadEdit> ThreadEdits { get; set; }

        public DbSet<ThreadCategory> ThreadCategories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostEdit> PostEdits { get; set; }

        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<PostCommentEdit> PostCommentEdits { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<ForumSection> ForumSections { get; set; }

        public DbSet<LoggingEntry> LoggingEntries { get; set; }

        public DbSet<LoggingType> LoggingTypes { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<MicroblogEntry> MicroblogEntries { get; set; }

        public DbSet<MicroblogEdit> MicroblogEntryEdits { get; set; }

        public DbSet<MicroblogComment> MicroblogComments { get; set; }

        public DbSet<MicroblogCommentEdit> MicroblogCommentEdits { get; set; }

        public DbSet<EmailLog> EmailLogs { get; set; }

        public DbSet<EmailMessageType> EmailMessageTypes { get; set; }

        public DbSet<UserFile> DownloadFileLog { get; set; }
    }
}
