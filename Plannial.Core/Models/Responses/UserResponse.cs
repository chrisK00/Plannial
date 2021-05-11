using System;

namespace Plannial.Core.Models.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}