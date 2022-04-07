using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recommendating.Api.Controllers;
using Recommendating.Api.Entities;
using Recommendating.Api.Repositories;
using Xunit;

namespace Recommendating.UnitTests;

public class UserControllerTests
{
    [Fact]
    public void GetUser_WithNonExistingGuid_ReturnsNotFound()
    {
        // Arrange
        var repositoryStub = new Mock<IUserRepository>();
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<Guid>()))
            .Returns((User) null);

        var controller = new UserController(repositoryStub.Object);

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

        var repositoryStub = new Mock<IUserRepository>();
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<Guid>()))
            .Returns(expectedUser);

        var controller = new UserController(repositoryStub.Object);

        // Act
        var result = controller.GetUser(Guid.NewGuid());

        // Assert
        result.Value.Should().BeEquivalentTo(expectedUser, options => options.ExcludingMissingMembers());
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