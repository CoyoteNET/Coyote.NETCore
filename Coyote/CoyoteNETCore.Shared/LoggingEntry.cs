using System;

namespace CoyoteNETCore.Shared
{
    public class LoggingEntry
    {
        private LoggingEntry()
        {

        }

        public LoggingEntry(LoggingType type, DateTime occuredAt, User user, bool succeeded, string _IPv4, string platformInfo)
        {
            Type = type;
            OccuredAt = occuredAt;
            User = user;
            Succeeded = succeeded;
            IPv4 = _IPv4;
            PlatformInfo = platformInfo;
        }

        public int Id { get; private set; }

        public LoggingType Type { get; private set; }

        public DateTime OccuredAt { get; private set; } = DateTime.Now;

        public User User { get; private set; }

        public bool Succeeded { get; private set; }

        public string IPv4 { get; private set; }

        public string PlatformInfo { get; private set; }
    }
}