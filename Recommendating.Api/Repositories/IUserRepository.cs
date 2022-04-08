using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserAsync(Guid id);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
}