using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserAsync(Guid id);
    Task<bool> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
}