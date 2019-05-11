﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoyoteNETCore.Shared
{
    public class User
    {
        private User()
        {

        }

        public User(string name, string email, string passwordHash, string passwordSalt)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public int Id { get; }

        public string Name { get; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;

        public DateTime? EmailConfirmationDate { get; set; }

        public string PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }

        public File Avatar { get; set; }

        public string Description { get; set; }

        public Role Role { get; set; }

        public DateTime RegisteredAt { get; } = DateTime.Now;

        public bool IsDeletionRequested { get; set; } = false;

        public DateTime? DeletionDate { get; set; }

        public LoggingEntry LastFailedLogginAttempt
        {
            get
            {
                return _LoggingInAttempts
                       .OrderByDescending(x => x.OccuredAt)
                       .FirstOrDefault(x => !x.Succeeded);
            }
        }

        public LoggingEntry SuccessfulLogginAttempt
        {
            get
            {
                return _LoggingInAttempts
                       .OrderByDescending(x => x.OccuredAt)
                       .FirstOrDefault(x => x.Succeeded);
            }
        }


        public ICollection<LoggingEntry> LoggingInAttempts { get; } = new List<LoggingEntry>();

        public ICollection<Post> Posts { get; } = new List<Post>();

        public ICollection<Notification> Notifications { get; } = new List<Notification>();

        public ICollection<UserFile> DownloadedFilesLog { get; } = new List<UserFile>();
    }
}
