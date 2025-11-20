using Microsoft.EntityFrameworkCore;
using Reski.Infrastructure.Context;

namespace ReskiTests.TestHelpers;

public static class DbContextHelper
{
    public static AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}