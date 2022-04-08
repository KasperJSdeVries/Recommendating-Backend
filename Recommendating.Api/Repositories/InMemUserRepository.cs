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

    public User? GetUser(Guid id)
    {
        return _users.SingleOrDefault(user => user.Id == id);
    }

    public void CreateUser(User user)
    {
        _users.Add(user);
    }
}