using com.itransition.final.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace com.itransition.final.Models.UserData;

public sealed class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime RegisterDateTime { get; set; }
    public Status Status { get; set; }
    public byte[]? Image { get; set; }

    public User()
    {
    }

    public User(RegisterModel registerModel)
    {
        Email = registerModel.Email;
        UserName = Email;
        FirstName = registerModel.FirstName;
        LastName = registerModel.LastName;
        RegisterDateTime = DateTime.Now;
        Status = Status.Active;
        Image = null;
    }
}