using System.ComponentModel.DataAnnotations;
using com.itransition.final.Models;

namespace com.itransition.final.ViewModels.Reviews.ReviewDetails;

public class UpdateCommentModel
{
    [Required(ErrorMessage = "Required field!")]
    [Display(Name = "Comment Text")]
    [DataType(DataType.Text)]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Invalid input length!")]
    public string Text { get; set; } = null!;
}