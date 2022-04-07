using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public interface IUserRepository
{
    public User? GetUser(Guid id);
}