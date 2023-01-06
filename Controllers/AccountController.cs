using System.Net;
using System.Runtime.Loader;
using com.itransition.final.Models;
using com.itransition.final.Models.UserData;
using com.itransition.final.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace com.itransition.final.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginModel());
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginModel);
        }

        var result = await _signInManager.PasswordSignInAsync(
            loginModel.Email, loginModel.Password, loginModel.RememberMe, false
        );
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Incorrect password or email!");
            return View(loginModel);
        }
        return RedirectToAction("Home", "Reviews");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        if (!ModelState.IsValid)
        {
            return View(registerModel);
        }

        var user = CreateFromModel(registerModel);
        var resultCreate = await _userManager.CreateAsync(user, registerModel.Password);
        var resultAssignRole = await _userManager.AddToRoleAsync(user, nameof(RoleTitle.User));
        if (!resultCreate.Succeeded && !resultAssignRole.Succeeded)
        {
            foreach (var identityError in resultCreate.Errors)
            {
                ModelState.AddModelError(string.Empty, identityError.Description);
            }

            return View(registerModel);
        }

        await _signInManager.SignInAsync(user, false);
        return RedirectToAction("Home", "Reviews");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "Reviews");
    }

    private User CreateFromModel(RegisterModel registerModel)
    {
        return new User
        {
            Email = registerModel.Email, UserName = registerModel.Email,
            FirstName = registerModel.FirstName, LastName = registerModel.LastName,
            RegisterDateTime = DateTime.Now, Status = Status.Active
        };
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ExternalLogin(string provider, string returnUrl = null)
    {
        return null;
    }

    private bool IsUserAvailableToLogin(User user)
    {
        return (user.Status != Status.Deleted) && (user.Status != Status.Blocked);
    }
}