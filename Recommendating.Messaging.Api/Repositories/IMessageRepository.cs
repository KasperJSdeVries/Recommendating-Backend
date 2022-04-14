using Recommendating.Messaging.Api.Models;

namespace Recommendating.Messaging.Api.Repositories;

public interface IMessageRepository
{
    Task<bool> CreateMessageAsync(Message message);
    Task<Message?> GetMessageAsync(Guid id);
}