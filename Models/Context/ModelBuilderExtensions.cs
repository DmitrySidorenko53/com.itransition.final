using com.itransition.final.Models.UserData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace com.itransition.final.Models.Context;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder builder)
    { 
        var roles = GetDefaultRoles();
        // var admins = GetDefaultAdmin();
        // var adminsRoles = GetAdminRoles(roles, admins);

        builder.Entity<IdentityRole>().HasData(roles);
        // builder.Entity<User>().HasData(admins);
        // admins.ForEach(HashPassword);
        // builder.Entity<IdentityUserRole<string>>().HasData(adminsRoles);
    }

    private static void HashPassword(User user)
    {
        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Glory2010");
    }

    private static List<IdentityUserRole<string>> GetAdminRoles(
        List<IdentityRole> identityRoles, List<User> users)
    {
        return new List<IdentityUserRole<string>>()
        {
            new()
            {
                UserId = users[0].Id,
                RoleId = identityRoles.FirstOrDefault(r => r.Name.Equals(RoleTitle.Admin.ToString()))?.Id!
            },
            new()
            {
                UserId = users[0].Id,
                RoleId = identityRoles.FirstOrDefault(r => r.Name.Equals(RoleTitle.User.ToString()))?.Id!
            }
        };
    }

    private static List<User> GetDefaultAdmin()
    {
        return new List<User>()
        {
            new()
            {
                Email = "dimasidorenko53@gmail.com",
                UserName = "dimasidorenko53@gmail.com",
                FirstName = "Dmitry",
                LastName = "Sidorenko",
                Status = Status.Active,
                RegisterDateTime = new DateTime(2022, 12, 20, 20, 20, 20),
                Image = null
            }
        };
    }

    private static List<IdentityRole> GetDefaultRoles()
    {
        return new List<IdentityRole>()
        {
            new() {Name = RoleTitle.Admin.ToString(), NormalizedName = RoleTitle.Admin.ToString().ToUpper()},
            new() {Name = RoleTitle.User.ToString(), NormalizedName = RoleTitle.User.ToString().ToUpper()}
        };
    }
}