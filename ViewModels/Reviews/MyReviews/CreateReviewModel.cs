using System.ComponentModel.DataAnnotations;
using com.itransition.final.Models.ReviewModels;

namespace com.itransition.final.ViewModels.Reviews.MyReviews;

public class CreateReviewModel
{
    [Required(ErrorMessage = "Required field!")]
    [DataType(DataType.Text)]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Invalid input length!")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Required field!")]
    [DataType(DataType.Text)]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Invalid input length!")]
    public string ProductTitle { get; set; } = null!;
    [Required(ErrorMessage = "Required field!")]
    public Category Category { get; set; }
    [Required(ErrorMessage = "Required field!")]
    [DataType(DataType.Text)]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Invalid input length!")]
    public string Text { get; set; } = null!;
}