namespace Recommendating.Api.Dtos;

public record UserDto(Guid Id, string Name, DateTimeOffset CreatedDate);

public record CreateUserDto(string Name, string Password);

public record UpdateUserNameDto(string Password, string Name);

public record UpdateUserPasswordDto(string OldPassword, string NewPassword);