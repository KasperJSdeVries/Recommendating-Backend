using Microsoft.AspNetCore.Mvc;
using Recommendating.Api.Dtos;
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
}