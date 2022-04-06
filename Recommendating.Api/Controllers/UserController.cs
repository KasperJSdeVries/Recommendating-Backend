using Microsoft.AspNetCore.Mvc;
using Recommendating.Api.Dtos;

namespace Recommendating.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class UserController : Controller
{
    [HttpGet("{id}")]
    public ActionResult<UserDto> GetUser(Guid id)
    {
        return NotFound();
    }
}