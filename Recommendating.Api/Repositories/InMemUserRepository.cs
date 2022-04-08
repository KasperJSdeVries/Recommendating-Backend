using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public class InMemUserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User
        {
            Id = Guid.NewGuid(),
            Name = "TestUser",
            Password = Guid.NewGuid().ToString(),
            CreatedDate = DateTimeOffset.Now
        }
    };

    public Task<User?> GetUserAsync(Guid id)
    {
        return Task.FromResult(_users.SingleOrDefault(user => user.Id == id));
    }

    public Task CreateUserAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user)
    {
        var index = _users.FindIndex(existingUser => existingUser.Id == user.Id);
        _users[index] = user;
        return Task.CompletedTask;
    }
}