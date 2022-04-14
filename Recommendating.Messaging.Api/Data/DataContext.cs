using Microsoft.EntityFrameworkCore;
using Recommendating.Messaging.Api.Models;

namespace Recommendating.Messaging.Api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }
}