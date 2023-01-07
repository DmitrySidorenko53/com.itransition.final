using com.itransition.final.Models.ReviewModels;

namespace com.itransition.final.ViewModels.Reviews.MyReviews;

public class MyReviewsModel
{
    public CreateReviewModel? CreateReviewModel { get; set; }
    public SortModel? SortModel { get; set; }
    public List<Review>? Reviews { get; set; }
}