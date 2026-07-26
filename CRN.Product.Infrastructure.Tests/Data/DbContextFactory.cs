using CRN.Product.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRN.Product.Infrastructure.Tests.Data;

public static class DbContextFactory
{
    public static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}