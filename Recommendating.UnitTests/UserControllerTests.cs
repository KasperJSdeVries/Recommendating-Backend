using System;
using System.Threading.Tasks;
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
    public async Task GetUserAsync_WithNonExistingGuid_ReturnsNotFound()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync((User) null);

        var controller = new UserController(_repositoryStub.Object);

        // Act
        var result = await controller.GetUserAsync(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetUserAsync_WithExistingUser_ReturnsRequestedUser()
    {
        // Arrange
        var expectedUser = CreateRandomUser();

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedUser);

        var controller = new UserController(_repositoryStub.Object);

        // Act
        var result = await controller.GetUserAsync(Guid.NewGuid());

        // Assert
        result.Value.Should().BeEquivalentTo(expectedUser, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public async Task CreateUserAsync_WithUserToCreate_ReturnsCreatedUser()
    {
        // Arrange
        var userToCreate = new CreateUserDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var controller = new UserController(_repositoryStub.Object);

        // Act
        var result = await controller.CreateUserAsync(userToCreate);

        // Assert
        var createdItem = (result.Result as CreatedAtActionResult).Value as UserDto;
        userToCreate.Should().BeEquivalentTo(createdItem, options => options.ExcludingMissingMembers());
        createdItem.Id.Should().NotBeEmpty();
        createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task UpdateUserNameAsync_WithExistingUser_ReturnsNoContent()
    {
        // Arrange
        var existingUserPassword = Guid.NewGuid().ToString();
        var existingUser = CreateRandomUser();

        var existingUserId = existingUser.Id;
        existingUser.Password = UserController.ComputeHash(existingUserPassword);

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingUser);

        var controller = new UserController(_repositoryStub.Object);

        var updatedUser = new UpdateUserNameDto(existingUserPassword, Guid.NewGuid().ToString());

        // Act
        var result = await controller.UpdateUserNameAsync(existingUserId, updatedUser);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateUserNameAsync_WithNonExistingUser_ReturnsNotFound()
    {
        // Arrange
        var controller = new UserController(_repositoryStub.Object);

        var updatedUser = new UpdateUserNameDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        // Act
        var result = await controller.UpdateUserNameAsync(Guid.NewGuid(), updatedUser);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateUserNameAsync_WithWrongPassword_ReturnsUnauthorised()
    {
        // Arrange
        var existingUser = CreateRandomUser();

        var existingUserId = existingUser.Id;
        existingUser.Password = UserController.ComputeHash(existingUser.Password);

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingUser);

        var controller = new UserController(_repositoryStub.Object);

        var updatedUser = new UpdateUserNameDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        // Act
        var result = await controller.UpdateUserNameAsync(existingUserId, updatedUser);

        // Assert
        result.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public async Task UpdatePasswordAsync_WithNonExistingUser_ReturnsNotFound()
    {
        // Arrange
        var controller = new UserController(_repositoryStub.Object);

        var updatedUser = new UpdateUserPasswordDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        // Act
        var result = await controller.UpdateUserPasswordAsync(Guid.NewGuid(), updatedUser);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdatePasswordAsync_WithWrongUserName_ReturnsUnauthorised()
    {
        // Arrange
        var existingUser = CreateRandomUser();

        var existingUserId = existingUser.Id;
        existingUser.Password = UserController.ComputeHash(existingUser.Password);

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingUser);

        var controller = new UserController(_repositoryStub.Object);

        var updatedUser = new UpdateUserPasswordDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        // Act
        var result = await controller.UpdateUserPasswordAsync(existingUserId, updatedUser);

        // Assert
        result.Should().BeOfType<UnauthorizedResult>();
    }


    [Fact]
    public async Task UpdatePasswordAsync_WithExistingUser_ReturnsNoContent()
    {
        // Arrange
        var existingUserPassword = Guid.NewGuid().ToString();
        var existingUser = CreateRandomUser();

        var existingUserId = existingUser.Id;
        existingUser.Password = UserController.ComputeHash(existingUserPassword);

        _repositoryStub.Setup(repo => repo.GetUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingUser);

        var controller = new UserController(_repositoryStub.Object);

        var updatedUser = new UpdateUserPasswordDto(existingUserPassword, Guid.NewGuid().ToString());

        // Act
        var result = await controller.UpdateUserPasswordAsync(existingUserId, updatedUser);

        // Assert
        result.Should().BeOfType<NoContentResult>();
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