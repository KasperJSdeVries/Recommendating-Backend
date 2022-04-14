using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public interface IUserRepository
{
    public Task<User> GetUserAsync(Guid id);
    public Task<User?> GetUserAsync(string email);
    Task<bool> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> AuthenticateUserAsync(string email, string password);
}