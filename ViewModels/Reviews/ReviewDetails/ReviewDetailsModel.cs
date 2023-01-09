using com.itransition.final.Models;
using com.itransition.final.Models.ReviewModels;

namespace com.itransition.final.ViewModels.Reviews.ReviewDetails;

public class ReviewDetailsModel
{
    public EditReviewModel EditReviewModel { get; set; }
    public CreateCommentModel CreateCommentModel { get; set; }
    public UpdateCommentModel UpdateCommentModel { get; set; }
    
    public Review? Review { get; set; }
    public List<Comment> Comments { get; set; }
    public AccessMode AccessMode { get; set; }
}