using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendating.Api.Entities;

public class User
{
    [Key] public Guid Id { get; init; }

    [Index(IsUnique = true)]
    [EmailAddress]
    public string Email { get; set; }

    public string Name { get; set; }
    public string Password { get; set; }
    public DateTimeOffset CreatedDate { get; init; }
}