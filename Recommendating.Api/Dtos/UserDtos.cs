using System.ComponentModel.DataAnnotations;

namespace Recommendating.Api.Dtos;

public record UserDto(Guid Id, string Name, DateTimeOffset CreatedDate);

public record CreateUserDto([MaxLength(32)] string Name, [MaxLength(32)] string Password);

public record UpdateUserNameDto(string Password, [MaxLength(32)] string Name);

public record UpdateUserPasswordDto(string OldPassword, [MaxLength(32)] string NewPassword);