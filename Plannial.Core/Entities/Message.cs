using System;

namespace Plannial.Core.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int RecipientId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; } = DateTime.UtcNow;
        public DateTime? DateRead { get; set; }
    }
}
