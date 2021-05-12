using System;

namespace Plannial.Core.Models.Responses
{
    public class MessageResponse
    {
        public int Id { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; } = DateTime.UtcNow;
        public DateTime? DateRead { get; set; }
    }
}
