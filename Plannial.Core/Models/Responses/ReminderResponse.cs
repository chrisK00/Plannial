using System;
using Plannial.Core.Helpers;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Models.Responses
{
    public class ReminderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Category Category { get; set; }
    }
}