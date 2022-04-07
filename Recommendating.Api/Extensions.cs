using Recommendating.Api.Dtos;
using Recommendating.Api.Entities;

namespace Recommendating.Api;

public static class Extensions
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto(user.Id, user.Name, user.CreatedDate);
    }
}