using System.Diagnostics.CodeAnalysis;

namespace Recommendating.Api.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}