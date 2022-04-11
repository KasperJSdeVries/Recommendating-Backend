using System.ComponentModel.DataAnnotations;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Dtos;

public record MessageDto(Guid Id, Guid Sender, Guid Receiver, string Text, MessageStatus Status,
    DateTimeOffset SentDate);

public record CreateMessageDto(Guid Sender, Guid Receiver, [MaxLength(4096)] string Text);