using System.ComponentModel.DataAnnotations;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;

namespace com.itransition.final.ViewModels.Reviews.ReviewDetails;

public class CommentModel
{
    [Required(ErrorMessage = "Required field!")]
    [Display(Name = "Comment Text")]
    [DataType(DataType.Text)]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Invalid input length!")]
    public string Text { get; set; } = null!;
    public User Author { get; set; }
    public Review Review { get; set; }
}