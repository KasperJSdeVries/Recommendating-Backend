using Microsoft.AspNetCore.Mvc;
using Recommendating.Login.Api.DTOs;

namespace Recommendating.Login.Api.Services;

public interface IOAuthService
{
    Task<ActionResult<AccessTokenResponse>> ProcessPasswordGrantAsync(string? email, string? password);
    Task<ActionResult<AccessTokenResponse>> RefreshAccessTokenAsync(string? refreshToken);
}