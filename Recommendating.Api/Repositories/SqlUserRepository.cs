using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Data;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public class SqlUserRepository : IUserRepository
{
    private readonly DataContext _context;

    public SqlUserRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }
}