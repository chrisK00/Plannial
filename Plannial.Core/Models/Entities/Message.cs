using System;

namespace Plannial.Core.Models.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public Guid RecipientId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; } = DateTime.UtcNow;                            
        public DateTime? DateRead { get; set; }
    }
}
