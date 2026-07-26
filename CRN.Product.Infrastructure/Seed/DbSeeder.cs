using CRN.Product.Domain.Entities;
using CRN.Product.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace CRN.Product.Infrastructure.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Users.Any())
            return;

        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            Username = "admin",
            Role = "Admin"
        };

        user.PasswordHash =
            hasher.HashPassword(user, "Admin@123");

        context.Users.Add(user);

        await context.SaveChangesAsync();
    }
}