using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Data;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public class SqlUserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public SqlUserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _dataContext.Users.SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task CreateUserAsync(User user)
    {
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _dataContext.Users.Update(user);
        await _dataContext.SaveChangesAsync();
    }
}