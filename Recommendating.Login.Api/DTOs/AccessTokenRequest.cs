using Newtonsoft.Json;

namespace Recommendating.Login.Api.DTOs;

public record AccessTokenRequest
{
    public string? Code;

    [JsonProperty(PropertyName = "grant_type")]
    public string GrantType;

    public string? Password;

    [JsonProperty(PropertyName = "redirect_uri")]
    public string? RedirectUri;

    [JsonProperty(PropertyName = "refresh_token")]
    public string? RefreshToken;

    public string? Username;
}