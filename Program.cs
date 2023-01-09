using com.itransition.final.Models.Context;
using com.itransition.final.Models.UserData;
using com.itransition.final.Services;
using com.itransition.final.Services.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(connection)
);

builder.Services.AddIdentity<User, IdentityRole>(
        options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        }
    )
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();

/*builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddGoogle(options =>
    {
        options.ClientId = "reviews";
        options.ClientSecret = "reviews";
    })
    .AddFacebook(options =>
    {
        options.AppId = "reviews";
        options.AppSecret = "reviews";
    });*/


var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Reviews}/{action=Home}");
});

app.Run();