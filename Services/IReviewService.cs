using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using com.itransition.final.ViewModels;
using com.itransition.final.ViewModels.Reviews;

namespace com.itransition.final.Services;

public interface IReviewService
{
    List<Review> GetReviewsByUserId(string userId);
    List<Review> GetReviewBySortModel(SortModel sortModel);
    Review? GetReviewsById(int reviewId);
    Task CreateReview(ReviewModel reviewModel, User author);
}