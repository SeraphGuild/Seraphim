using System.Text.Json.Serialization;

namespace Discord.API;

internal class OAuthToken
{
    public OAuthToken()
    {
        AccessToken = string.Empty;
        ExpiresIn = default;
        Scope = string.Empty;
        TokenType = string.Empty;
        expirationTime = DateTimeOffset.MinValue;
    }

    public OAuthToken(string accessToken, int expiresIn, string scope, string tokenType)
    {
        this.AccessToken = accessToken;
        this.ExpiresIn = expiresIn;
        this.Scope = scope;
        this.TokenType = tokenType;
        this.expirationTime = DateTimeOffset.UtcNow.AddSeconds(ExpiresIn);
    }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; private set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; private set; }

    public string Scope { get; private set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; private set; }

    [JsonIgnore]
    private readonly DateTimeOffset expirationTime;

    public bool IsValid { 
        get { 
            return !string.IsNullOrEmpty(AccessToken) && 
            DateTimeOffset.UtcNow <= expirationTime.AddHours(-1);
        }
    }
}
