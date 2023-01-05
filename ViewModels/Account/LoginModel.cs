using System.ComponentModel.DataAnnotations;

namespace com.itransition.final.ViewModels.Account;

public class LoginModel
{
    [Required(ErrorMessage = "Required field!")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Required field!")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;
    
    [Display(Name = "Remember?")] 
    public bool RememberMe { get; set; }
}