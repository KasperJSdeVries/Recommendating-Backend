using System;
using Recommendating.Api.Entities;

namespace Recommendating.Api.Tests.Unit;

public static class TestHelpers
{
    public static User CreateRandomUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Password = Guid.NewGuid().ToString(),
            CreatedDate = DateTimeOffset.Now
        };
    }
}