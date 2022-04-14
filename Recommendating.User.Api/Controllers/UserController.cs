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

    // GET /user/{email}
    [HttpGet("{email}")]
    public async Task<ActionResult<UserDto>> GetUserAsync(string email)
    {
        var user = await _repository.GetUserAsync(email);
        return user is not null ? user.AsDto() : NotFound();
    }

    // POST /user
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto userDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = userDto.Name,
            Password = userDto.Password,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _repository.CreateUserAsync(user);

        // ReSharper disable once Mvc.ActionNotResolved
        return CreatedAtAction(nameof(GetUserAsync), new {id = user.Id}, user.AsDto());
    }

    // // PUT /user/{id}/name
    // [HttpPut("{id:guid}/name")]
    // public async Task<ActionResult> UpdateUserNameAsync(Guid id, UpdateUserNameDto updateDto)
    // {
    //     var user = await _repository.GetUserAsync(id);
    //     if (user is null)
    //         return NotFound();
    //
    //     if (user.Password != ComputeHash(updateDto.Password))
    //         return Unauthorized();
    //
    //     user.Name = updateDto.Name;
    //
    //     await _repository.UpdateUserAsync(user);
    //
    //     return NoContent();
    // }

    // // PUT /user/{id}/password
    // [HttpPut("{id:guid}/password")]
    // public async Task<ActionResult> UpdateUserPasswordAsync(Guid id, UpdateUserPasswordDto updateDto)
    // {
    //     var user = await _repository.GetUserAsync(id);
    //     if (user is null)
    //         return NotFound();
    //
    //     if (user.Password != ComputeHash(updateDto.OldPassword))
    //         return Unauthorized();
    //
    //     user.Password = ComputeHash(updateDto.NewPassword);
    //     await _repository.UpdateUserAsync(user);
    //
    //     return NoContent();
    // }

    // DELETE /user/{id}
    // [HttpDelete("{id:guid}")]
    // public async Task<ActionResult> DeleteUserAsync(Guid id, DeleteUserDto deleteDto)
    // {
    //     return Ok();
    // }
}