using com.itransition.final.Models.ReviewModels;

namespace com.itransition.final.ViewModels.Reviews.ReviewDetails;

public class ReviewDetailsModel
{
    public EditReviewModel EditReviewModel { get; set; }
    public CommentModel CommentModel { get; set; }
    public Review Review { get; set; }
}