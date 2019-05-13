using System;

namespace CoyoteNETCore.Shared
{
    public class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public DateTime CreationDate { get; protected set; } = DateTime.Now;

        public DateTime? ModifiedAt { get; set; }

        public User ModifiedBy { get; set; }
    }
}