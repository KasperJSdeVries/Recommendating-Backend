using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Recommendating.Api.Dtos;
using Recommendating.Api.Entities;
using Recommendating.Api.Repositories;

namespace Recommendating.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class UserController : Controller
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    // GET /user/{id}
    [HttpGet("{id}")]
    public ActionResult<UserDto> GetUser(Guid id)
    {
        var user = _repository.GetUser(id);
        return user is not null ? user.AsDto() : NotFound();
    }

    // POST /user
    [HttpPost]
    public ActionResult<UserDto> CreateUser(CreateUserDto userDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = userDto.Name,
            Password = ComputeHash(userDto.Password),
            CreatedDate = DateTimeOffset.UtcNow
        };

        _repository.CreateUser(user);

        return CreatedAtAction(nameof(GetUser), new {id = user.Id}, user.AsDto());
    }

    // PUT /user/{id}/name
    [HttpPut("{id}/name")]
    public ActionResult UpdateUserName(Guid id, UpdateUserNameDto updateDto)
    {
        var user = _repository.GetUser(id);
        if (user is null)
            return NotFound();

        if (user.Password != ComputeHash(updateDto.Password))
            return Unauthorized();

        user.Name = updateDto.Name;

        _repository.UpdateUser(user);

        return NoContent();
    }

    public static string ComputeHash(string data)
    {
        using var hash = SHA256.Create();
        var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(data));

        var builder = new StringBuilder();
        foreach (var b in bytes) builder.Append(b.ToString("x2"));

        return builder.ToString();
    }
}