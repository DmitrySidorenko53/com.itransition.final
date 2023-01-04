using Microsoft.AspNetCore.Identity;

namespace com.itransition.final.Models.UserData;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime RegisterDateTime { get; set; }
    public Status Status { get; set; }
    public byte[]? Image { get; set; } = null!;
}