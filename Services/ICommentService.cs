using com.itransition.final.Models;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using com.itransition.final.ViewModels.Reviews.ReviewDetails;

namespace com.itransition.final.Services;

public interface ICommentService
{
    List<Comment> GetCommentsByReviewId(int reviewId);
    Task CreateComment(CreateCommentModel commentModel, User Author, Review? CurrentReview);
    Task UpdateComment(UpdateCommentModel commentModel, int commentId);
    Task ChangeStatus(Status status, int commentId);
}