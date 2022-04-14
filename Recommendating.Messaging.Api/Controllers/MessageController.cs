using Microsoft.AspNetCore.Mvc;
using Recommendating.Messaging.Api.DTOs;
using Recommendating.Messaging.Api.Extensions;
using Recommendating.Messaging.Api.Models;
using Recommendating.Messaging.Api.Repositories;

namespace Recommendating.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class MessageController : Controller
{
    private readonly IMessageRepository _messageRepository;

    public MessageController(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }


    // POST /message
    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessageAsync(CreateMessageDto messageDto)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Sender = messageDto.Sender,
            Receiver = messageDto.Receiver,
            SentDate = DateTimeOffset.UtcNow,
            Status = MessageStatus.Sent,
            Text = messageDto.Text
        };

        await _messageRepository.CreateMessageAsync(message);

        // ReSharper disable once Mvc.ActionNotResolved
        return CreatedAtAction(nameof(GetMessageAsync), new {id = message.Id}, message.AsDto());
    }

    // GET /message/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MessageDto>> GetMessageAsync(Guid id)
    {
        var message = await _messageRepository.GetMessageAsync(id);
        return message is not null ? message.AsDto() : NotFound();
    }
}