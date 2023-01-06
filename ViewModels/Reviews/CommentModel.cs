using System.ComponentModel.DataAnnotations;

namespace com.itransition.final.ViewModels.Reviews;

public class CommentModel
{
    [Required(ErrorMessage = "Required field!")]
    [Display(Name = "Comment Text")]
    [DataType(DataType.Text)]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Invalid input length!")]
    public string Text { get; set; } = null!;
}