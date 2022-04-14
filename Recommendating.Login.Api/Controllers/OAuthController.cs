using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Recommendating.Login.Api.DTOs;
using Recommendating.Login.Api.Services;

namespace Recommendating.Login.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class OAuthController : Controller
{
    private readonly IOAuthService _authService;
    private readonly IConfiguration _configuration;

    public OAuthController(IConfiguration configuration, IOAuthService authService)
    {
        _configuration = configuration;
        _authService = authService;
    }

    // POST oauth/token
    [HttpPost("token")]
    public async Task<ActionResult<AccessTokenResponse>> AccessRequestAsync(AccessTokenRequest request)
    {
        switch (request.GrantType)
        {
            case "password": return await _authService.ProcessPasswordGrantAsync(request.Username, request.Password);
            case "refresh_token": return await _authService.RefreshAccessTokenAsync(request.RefreshToken);
            case "code": return CreateUnsupportedGrantError();
            default: return CreateUnsupportedGrantError();
        }
    }

    private BadRequestObjectResult CreateUnsupportedGrantError()
    {
        return BadRequest(new JsonObject
        {
            new("error", "unsupported_grant_type"),
            new("error_description", "We only support 'password' grants")
        });
    }
}