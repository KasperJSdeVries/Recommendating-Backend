using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public class InMemUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task<User?> GetUserAsync(Guid id)
    {
        return Task.FromResult(_users.SingleOrDefault(user => user.Id == id));
    }

    public Task<bool> CreateUserAsync(User user)
    {
        _users.Add(user);
        return Task.FromResult(true);
    }

    public Task<bool> UpdateUserAsync(User user)
    {
        var index = _users.FindIndex(existingUser => existingUser.Id == user.Id);
        _users[index] = user;
        return Task.FromResult(true);
    }
}