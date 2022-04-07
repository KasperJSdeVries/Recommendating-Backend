using System.Diagnostics.CodeAnalysis;

namespace Recommendating.Api.Entities;

public class User
{
    public Guid Id { get; set; }

    [NotNull] public string Name { get; set; }

    public string Password { get; set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; }
}