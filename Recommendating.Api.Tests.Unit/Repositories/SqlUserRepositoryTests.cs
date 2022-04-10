using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Recommendating.Api.Data;
using Recommendating.Api.Entities;
using Recommendating.Api.Repositories;
using Xunit;

namespace Recommendating.Api.Tests.Unit.Repositories;

public class SqlUserRepositoryTests : IDisposable
{
    private readonly DataContext _context;

    public SqlUserRepositoryTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new DataContext(dbOptions.Options);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task CreateUserAsync_WithUser_ShouldStoreUserAndReturnTrue()
    {
        // Arrange
        var sut = new SqlUserRepository(_context);
        var user = TestHelpers.CreateRandomUser();

        // Act
        var result = await sut.CreateUserAsync(user);

        // Assert
        result.Should().BeTrue();

        var users = _context.Users.ToList();
        users.Should().ContainSingle();
    }

    [Fact]
    public async Task GetUserAsync_WithoutUserInDb_ShouldReturnNull()
    {
        // Arrange
        var sut = new SqlUserRepository(_context);

        // Act
        var result = await sut.GetUserAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetUserAsync_WithUserInDb_ShouldReturnUser()
    {
        // Arrange
        var existingUser = TestHelpers.CreateRandomUser();
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        var sut = new SqlUserRepository(_context);

        // Act
        var result = await sut.GetUserAsync(existingUser.Id);

        // Assert
        result.Should().BeEquivalentTo(existingUser);
    }
}