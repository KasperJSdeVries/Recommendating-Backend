namespace Recommendating.Api.Repositories;

public interface ITokenRepository
{
    Task<bool> AddTokenAsync(string token, int accessTokenExpiryDuration);
    Task<bool> IsValidTokenAsync(string token);
    Task<bool> InvalidateTokenAsync(string token);
}