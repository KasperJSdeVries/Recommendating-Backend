using System.ComponentModel.DataAnnotations;

namespace Recommendating.Api.Entities;

public class User
{
    [Key] public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}