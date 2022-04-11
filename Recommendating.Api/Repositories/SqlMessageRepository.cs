﻿using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Data;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Repositories;

public class SqlMessageRepository : IMessageRepository
{
    private readonly DataContext _context;

    public SqlMessageRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateMessageAsync(Message? message)
    {
        await _context.Messages.AddAsync(message);

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

    public async Task<Message?> GetMessageAsync(Guid id)
    {
        return await _context.Messages.SingleOrDefaultAsync(message => message.Id == id);
    }
}