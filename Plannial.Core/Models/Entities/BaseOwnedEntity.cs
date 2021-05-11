using System;

namespace Plannial.Core.Models.Entities
{
    public abstract class BaseOwnedEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
