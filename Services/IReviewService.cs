using com.itransition.final.Models;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using com.itransition.final.ViewModels;
using com.itransition.final.ViewModels.Reviews.MyReviews;

namespace com.itransition.final.Services;

public interface IReviewService
{
    List<Review> GetReviewsByAuthorId(string? userId);
    List<Review> GetReviewBySortModel(SortModel? sortModel);
    Task<Review> GetReviewById(int reviewId);
    Task CreateReview(CreateReviewModel reviewModel, User author);
    Task ChangeReviewStatus(Status status, int reviewId);
}