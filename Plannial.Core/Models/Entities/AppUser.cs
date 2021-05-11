using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plannial.Core.Models.Entities
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Message> MessagesRecieved { get; set; } = new List<Message>();
        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();
    }
}
