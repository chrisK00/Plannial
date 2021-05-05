using System;
using Plannial.Core.Helpers;

namespace Plannial.Core.Requests
{
    public class AddReminderRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority{ get; set; }
    }
}
