using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Data;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public class SqlUserRepository : IUserRepository
{
    private readonly DataContext _context;

    public SqlUserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        return await _context.Users.SingleAsync(user => user.Id == id);
    }

    public async Task<User?> GetUserAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(user => user.Email == email);
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

    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.Email == email);
        if (user is null) return false;
        return user.Password == password;
    }
}