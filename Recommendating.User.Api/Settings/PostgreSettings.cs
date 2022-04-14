namespace Recommendating.Api.Settings;

public class PostgreSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public bool Pooling { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }

    public string ConnectionString =>
        $"User ID={UserId};Password={Password};Host={Host};Port={Port};Database={Database};Pooling={Pooling}";
}