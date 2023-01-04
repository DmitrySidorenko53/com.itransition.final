using com.itransition.final.Models;
using com.itransition.final.Models.UserData;
using com.itransition.final.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace com.itransition.final.Services.Impl;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task Register(RegisterModel registerModel)
    {
        var user = CreateUserFromModel(registerModel);
        var result = await _userManager.CreateAsync(user, registerModel.Password);
        
    }

    private User CreateUserFromModel(RegisterModel registerModel)
    {
        return new User
        {
            Email = registerModel.Email,
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,
            Status = Status.Active,
            RegisterDateTime = DateTime.Now
        };
    }
}