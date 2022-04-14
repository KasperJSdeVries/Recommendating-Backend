using Newtonsoft.Json;

namespace Recommendating.Login.Api.DTOs;

public record AccessTokenResponse
{
    [JsonProperty(PropertyName = "access_token")]
    public string AccessToken;

    [JsonProperty(PropertyName = "expires_in")]
    public decimal ExpiresIn;

    [JsonProperty(PropertyName = "refresh_token")]
    public string RefreshToken;

    [JsonProperty(PropertyName = "token_type")]
    public string TokenType;
}