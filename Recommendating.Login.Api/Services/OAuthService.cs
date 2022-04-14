using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Recommendating.Api.Repositories;
using Recommendating.Login.Api.DTOs;

namespace Recommendating.Login.Api.Services;

public class OAuthService : IOAuthService
{
    private const int AccessTokenExpiryDuration = 600;
    private const int RefreshTokenExpiryDuration = 604800;
    private readonly IConfiguration _configuration;
    private readonly ITokenRepository _tokenRepository;

    public OAuthService(IConfiguration configuration, ITokenRepository tokenRepository)
    {
        _configuration = configuration;
        _tokenRepository = tokenRepository;
    }

    public async Task<ActionResult<AccessTokenResponse>> ProcessPasswordGrantAsync(string? email, string? password)
    {
        if (email is null || password is null) return CreateInvalidGrantError();

        return new OkObjectResult(await CreateAccessTokenResponse(email));
    }

    public async Task<ActionResult<AccessTokenResponse>> RefreshAccessTokenAsync(string? refreshToken)
    {
        if (refreshToken is null) return CreateInvalidGrantError();

        if (!await _tokenRepository.IsValidTokenAsync(refreshToken)) return CreateInvalidGrantError();

        var email = RetrieveEmailFromToken(refreshToken);
        if (email is null) return CreateInvalidGrantError();

        await _tokenRepository.InvalidateTokenAsync(refreshToken);

        return new OkObjectResult(await CreateAccessTokenResponse(email));
    }

    private async Task<AccessTokenResponse> CreateAccessTokenResponse(string email)
    {
        var accessToken = CreateAccessToken(email);
        var refreshToken = CreateRefreshToken(email);

        await _tokenRepository.AddTokenAsync(accessToken, AccessTokenExpiryDuration);
        await _tokenRepository.AddTokenAsync(refreshToken, RefreshTokenExpiryDuration);

        var tokenResponse = new AccessTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "bearer",
            ExpiresIn = AccessTokenExpiryDuration,
            RefreshToken = refreshToken
        };
        return tokenResponse;
    }

    private static string? RetrieveEmailFromToken(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        if (!jwtHandler.CanReadToken(token)) return null;

        var claims = jwtHandler.ReadJwtToken(token).Claims;
        return claims.SingleOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }

    private string CreateAccessToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
        };

        return CreateToken(claims, AccessTokenExpiryDuration);
    }

    private string CreateRefreshToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Sid, GetRandomString())
        };

        return CreateToken(claims, RefreshTokenExpiryDuration);
    }

    private string CreateToken(Claim[] claims, int expiryDuration)
    {
        var encoding = Encoding.UTF8.GetBytes(_configuration["SecurityKey"]);
        var key = new SymmetricSecurityKey(encoding);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _configuration["Issuer"],
            expires: DateTime.Now.AddSeconds(expiryDuration),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private BadRequestObjectResult CreateInvalidGrantError()
    {
        return new BadRequestObjectResult(new JsonObject
        {
            new("error", "invalid_grant")
        });
    }

    private static string GetRandomString(int size = 32)
    {
        var randomNumber = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}