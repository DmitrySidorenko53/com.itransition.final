using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace com.itransition.final.Models.Context;

public sealed class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<ReviewRating> ReviewRatings { get; set; } = null!;

    public ApplicationContext(DbContextOptions options) :
        base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Seed();
        base.OnModelCreating(builder);
    }
}