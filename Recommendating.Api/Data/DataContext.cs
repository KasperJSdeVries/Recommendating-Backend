using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Message?> Messages { get; set; }
}