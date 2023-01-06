using System.ComponentModel.DataAnnotations;

namespace com.itransition.final.ViewModels.Account;

public class RegisterModel
{
    [Required(ErrorMessage = "Required field!")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Required field!")]
    [Display(Name = "Full Name")]
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid input length!")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Required field!")]
    [Display(Name = "Last Name")]
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid input length!")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Required field!")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Required field!")]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string PasswordConfirm { get; set; } = null!;

   // public byte[] Image { get; set; } = null!;
}