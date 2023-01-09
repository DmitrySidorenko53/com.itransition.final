using com.itransition.final.Models;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using com.itransition.final.Services;
using com.itransition.final.ViewModels.Reviews.ReviewDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace com.itransition.final.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IReviewService _reviewService;
    private readonly UserManager<User> _userManager;

    public CommentController(ICommentService commentService, UserManager<User> userManager,
        IReviewService reviewService)
    {
        _commentService = commentService;
        _userManager = userManager;
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentModel commentModel)
    {
        var currentReviewId = Int32.Parse(HttpContext.Session.GetString("reviewId") ?? string.Empty);
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var author = await _userManager.GetUserAsync(HttpContext.User);
        var review = await _reviewService.GetReviewById(currentReviewId);
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ReviewDetails", "Reviews", new {reviewId = currentReviewId});
        }

        await _commentService.CreateComment(commentModel, author, review);
        return RedirectToAction("ReviewDetails", "Reviews", new {reviewId = currentReviewId});
    }

    [HttpPost]
    public async Task<IActionResult> EditComment(UpdateCommentModel commentModel, string commentId)
    {
        var currentReviewId = Int32.Parse(HttpContext.Session.GetString("reviewId") ?? string.Empty);
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }
        if (!ModelState.IsValid)
        {
            return RedirectToAction("ReviewDetails", "Reviews", new {reviewId = currentReviewId});
        }
        
        await _commentService.UpdateComment(commentModel, Int32.Parse(commentId));
        return RedirectToAction("ReviewDetails", "Reviews", new {reviewId = currentReviewId});
    }

    [HttpPost]
    public async Task<IActionResult> DeleteComment(string commentId)
    {
        var currentReviewId = Int32.Parse(HttpContext.Session.GetString("reviewId") ?? string.Empty);
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }
        await _commentService.ChangeStatus(Status.Deleted, Int32.Parse(commentId));
        return RedirectToAction("ReviewDetails", "Reviews", new {reviewId = currentReviewId});
    }

    private async Task<bool> IsAuthor(User user)
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        return currentUser != null && currentUser.Id.Equals(user.Id);
    }
}