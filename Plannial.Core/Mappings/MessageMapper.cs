using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Mappings
{
    public static class MessageMapper
    {
        public static MessageResponse MapToMessageResponse(Message message)
        {
            return new MessageResponse
            {
                SenderId = message.SenderId,
                RecipientId = message.RecipientId,
                Content = message.Content,
                DateSent = message.DateSent,
                DateRead = message.DateRead,
                Id = message.Id
            };
        }
    }
}
