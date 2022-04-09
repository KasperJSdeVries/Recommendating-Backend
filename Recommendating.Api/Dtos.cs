using Recommendating.Api.Entities;

namespace Recommendating.Api.Dtos;

public record UserDto(Guid Id, string Name, DateTimeOffset CreatedDate);

public record CreateUserDto(string Name, string Password);

public record UpdateUserNameDto(string Password, string Name);

public record UpdateUserPasswordDto(string OldPassword, string NewPassword);

public record MessageDto(Guid Id, Guid Sender, Guid Receiver, string Message, MessageStatus Status,
    DateTimeOffset SentDate);

public record CreateMessageDto(Guid Sender, Guid Receiver, string Text);