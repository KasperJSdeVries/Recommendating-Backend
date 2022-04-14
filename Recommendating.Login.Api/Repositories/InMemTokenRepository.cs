namespace Recommendating.Api.Repositories;

public class InMemTokenRepository : ITokenRepository
{
    private readonly List<Token> _tokens = new();

    public Task<bool> AddTokenAsync(string token, int accessTokenExpiryDuration)
    {
        _tokens.Add(new Token(
            token,
            DateTimeOffset.UtcNow + TimeSpan.FromSeconds(accessTokenExpiryDuration)
        ));
        return Task.FromResult(true);
    }

    public Task<bool> IsValidTokenAsync(string token)
    {
        var t = _tokens.SingleOrDefault(t => t.Key == token);
        return t is null ? Task.FromResult(false) : Task.FromResult(t.Expiry > DateTimeOffset.UtcNow);
    }

    public Task<bool> InvalidateTokenAsync(string token)
    {
        var toRemove = _tokens.FirstOrDefault(t => t.Key == token);
        if (toRemove is not null) _tokens.Remove(toRemove);
        return Task.FromResult(true);
    }

    private record Token(string Key, DateTimeOffset Expiry);
}