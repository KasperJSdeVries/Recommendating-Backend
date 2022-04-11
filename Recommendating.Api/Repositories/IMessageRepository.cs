using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public interface IMessageRepository
{
    Task<bool> CreateMessageAsync(Message? message);
    Task<Message?> GetMessageAsync(Guid id);
}