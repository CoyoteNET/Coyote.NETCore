﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CoyoteNET.Shared.Entities
{
    public class User
    {
        private User()
        {

        }

        public User(string name, string email)//, string passwordHash, string passwordSalt)
        {
            Username = name;
            Email = email;
            //PasswordHash = passwordHash;
            //PasswordSalt = passwordSalt;
        }

        public int Id { get; private set; }

        public string Username { get; private set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;

        public DateTime? EmailConfirmationDate { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public File Avatar { get; set; }

        public string Description { get; set; }

        public Role Role { get; set; }

        public DateTime RegisteredAt { get; private set; } = DateTime.Now;

        public bool IsDeletionRequested { get; set; } = false;

        public DateTime? DeletionDate { get; set; }

        public LoggingEntry LastFailedLogginAttempt
        {
            get
            {
                return LoggingInAttempts
                       .OrderByDescending(x => x.OccuredAt)
                       .FirstOrDefault(x => !x.Succeeded);
            }
        }

        public LoggingEntry SuccessfulLogginAttempt
        {
            get
            {
                return LoggingInAttempts
                       .OrderByDescending(x => x.OccuredAt)
                       .FirstOrDefault(x => x.Succeeded);
            }
        }

        public ICollection<LoggingEntry> LoggingInAttempts { get; private set; } = new List<LoggingEntry>();

        public ICollection<Post> Posts { get; private set; } = new List<Post>();

        public ICollection<Notification> Notifications { get; private set; } = new List<Notification>();

        public ICollection<UserFile> DownloadedFilesLog { get; private set;} = new List<UserFile>();
    }
}
