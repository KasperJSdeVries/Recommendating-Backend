using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recommendating.Api.Controllers;
using Recommendating.Api.Dtos;
using Recommendating.Api.Entities;
using Recommendating.Api.Repositories;
using Xunit;

namespace Recommendating.UnitTests;

public class UserControllerTests
{
    private readonly Mock<IUserRepository> _repositoryStub = new();

    [Fact]
    public void GetUser_WithNonExistingGuid_ReturnsNotFound()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<Guid>()))
            .Returns((User) null);

        var controller = new UserController(_repositoryStub.Object);

        // Act
        var result = controller.GetUser(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void GetUser_WithExistingUser_ReturnsRequestedUser()
    {
        // Arrange
        var expectedUser = CreateRandomUser();

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<Guid>()))
            .Returns(expectedUser);

        var controller = new UserController(_repositoryStub.Object);

        // Act
        var result = controller.GetUser(Guid.NewGuid());

        // Assert
        result.Value.Should().BeEquivalentTo(expectedUser, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public void CreateUser_WithUserToCreate_ReturnsCreatedUser()
    {
        // Arrange
        var userToCreate = new CreateUserDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var controller = new UserController(_repositoryStub.Object);

        // Act
        var result = controller.CreateUser(userToCreate);

        // Assert
        var createdItem = (result.Result as CreatedAtActionResult).Value as UserDto;
        userToCreate.Should().BeEquivalentTo(createdItem, options => options.ExcludingMissingMembers());
        createdItem.Id.Should().NotBeEmpty();
        createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    private static User CreateRandomUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            CreatedDate = DateTimeOffset.Now
        };
    }
}