using Microsoft.AspNetCore.Mvc;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class MessageController : Controller
{
    [HttpGet("{id}")]
    public ActionResult<Message> GetMessage(Guid id)
    {
        return Ok();
    }
}