using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public interface IMessageRepository
{
    Task CreateMessageAsync(Message message);
}