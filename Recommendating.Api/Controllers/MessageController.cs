using Microsoft.AspNetCore.Mvc;
using Recommendating.Api.Dtos;
using Recommendating.Api.Entities;
using Recommendating.Api.Repositories;

namespace Recommendating.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class MessageController : Controller
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public MessageController(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }


    // POST /message
    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessageAsync(CreateMessageDto messageDto)
    {
        var sender = await _userRepository.GetUserAsync(messageDto.Sender);
        var receiver = await _userRepository.GetUserAsync(messageDto.Receiver);

        if (sender is null || receiver is null) return NotFound();

        var message = new Message
        {
            Id = Guid.NewGuid(),
            Sender = sender,
            Receiver = receiver,
            SentDate = DateTimeOffset.UtcNow,
            Status = MessageStatus.Sent,
            Text = messageDto.Text
        };

        await _messageRepository.CreateMessageAsync(message);

        // ReSharper disable once Mvc.ActionNotResolved
        return CreatedAtAction(nameof(GetMessageAsync), new {id = message.Id}, message.AsDto());
    }

    // GET /message/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetMessageAsync(Guid id)
    {
        return Ok();
    }
}