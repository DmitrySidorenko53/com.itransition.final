using com.itransition.final.Models.ReviewModels;

namespace com.itransition.final.ViewModels.Reviews;

public class ReviewModel
{
    public string Title { get; set; } = null!;
    public string ProductTitle { get; set; } = null!;
    public Category Category { get; set; }
    public string Text { get; set; } = null!;
}