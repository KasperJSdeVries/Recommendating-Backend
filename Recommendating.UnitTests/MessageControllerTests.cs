using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recommendating.Api.Controllers;
using Recommendating.Api.Dtos;
using Recommendating.Api.Repositories;
using Xunit;

namespace Recommendating.UnitTests;

public class MessageControllerTests
{
    private readonly Mock<IMessageRepository> _messageRepositoryStub = new();
    private readonly Mock<IUserRepository> _userRepositoryStub = new();

    [Fact]
    public async Task CreateMessageAsync_WithValidMessage_ReturnsCreatedMessage()
    {
        var sendingUser = TestHelpers.CreateRandomUser();
        var receivingUser = TestHelpers.CreateRandomUser();

        // Arrange
        _userRepositoryStub.Setup(repo => repo.GetUserAsync(It.Is<Guid>(id => id == sendingUser.Id)))
            .ReturnsAsync(sendingUser);
        _userRepositoryStub.Setup(repo => repo.GetUserAsync(It.Is<Guid>(id => id == receivingUser.Id)))
            .ReturnsAsync(receivingUser);

        var messageToCreate = new CreateMessageDto(sendingUser.Id, receivingUser.Id, Guid.NewGuid().ToString());

        var controller = new MessageController(_messageRepositoryStub.Object, _userRepositoryStub.Object);

        // Act
        var result = await controller.CreateMessageAsync(messageToCreate);

        // Assert
        var createdMessage = (result.Result as CreatedAtActionResult).Value as MessageDto;
        messageToCreate.Should().BeEquivalentTo(createdMessage, options => options.ExcludingMissingMembers());
        createdMessage.Id.Should().NotBeEmpty();
        createdMessage.SentDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task CreateMessageAsync_WithNonExistingUser_ReturnsNotFound()
    {
        // Arrange
        var messageToCreate = new CreateMessageDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid().ToString());

        var controller = new MessageController(_messageRepositoryStub.Object, _userRepositoryStub.Object);
        
        // Act
        var result = await controller.CreateMessageAsync(messageToCreate);
        
        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }
}