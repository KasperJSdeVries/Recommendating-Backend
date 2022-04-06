namespace Recommendating.Api.Dtos;

public record UserDto(Guid Id, string Name, DateTimeOffset CreatedDate);

public record UserCreateDto;