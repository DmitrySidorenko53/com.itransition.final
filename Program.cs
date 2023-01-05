using com.itransition.final.Models.Context;
using com.itransition.final.Models.UserData;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DeployConnection");

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
            options.SignIn.RequireConfirmedEmail = true;
        }
    )
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication(options =>
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
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run(
);