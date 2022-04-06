using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Recommendating.Api.Controllers;
using Xunit;

namespace Recommendating.UnitTests;

public class UserControllerTests
{
    [Fact]
    public void GetUser_WithNonExistingGuid_ReturnsNotFound()
    {
        // Arrange
        var controller = new UserController();

        // Act
        var result = controller.GetUser(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }
}