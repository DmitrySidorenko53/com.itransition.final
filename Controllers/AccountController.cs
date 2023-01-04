using com.itransition.final.Models.UserData;
using com.itransition.final.Services;
using com.itransition.final.Services.Impl;
using com.itransition.final.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace com.itransition.final.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginModel { ReturnUrl = returnUrl });
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public Task<IActionResult> Login(LoginModel loginModel)
    {
        return null;
    }
    
    [HttpPost]
    public Task<IActionResult> Register(RegisterModel registerModel)
    {
        return null;
    }
    
    [HttpPost]
    public Task<IActionResult> Logout()
    {
        return null;
    }
}