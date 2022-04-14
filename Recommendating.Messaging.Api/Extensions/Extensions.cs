using Recommendating.Messaging.Api.DTOs;
using Recommendating.Messaging.Api.Models;

namespace Recommendating.Messaging.Api.Extensions;

public static class Extensions
{
    public static MessageDto AsDto(this Message message)
    {
        return new MessageDto(
            message.Id,
            message.Sender,
            message.Receiver,
            message.Text,
            message.Status,
            message.SentDate
        );
    } 
}