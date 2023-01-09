using System.Security.Claims;
using com.itransition.final.Models;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using com.itransition.final.Services;
using com.itransition.final.ViewModels;
using com.itransition.final.ViewModels.Reviews.Home;
using com.itransition.final.ViewModels.Reviews.MyReviews;
using com.itransition.final.ViewModels.Reviews.ReviewDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace com.itransition.final.Controllers;

public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;
    private readonly UserManager<User> _userManager;
    private readonly ICommentService _commentService;

    public ReviewsController(IReviewService reviewService, UserManager<User> userManager,
        ICommentService commentService)
    {
        _reviewService = reviewService;
        _userManager = userManager;
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> ReviewDetails(int reviewId)
    {
        var review = await _reviewService.GetReviewById(reviewId);
        EditReviewModel editReviewModel = new EditReviewModel {AccessMode = AccessMode.ReadOnly};
        CreateCommentModel createCommentModel = new CreateCommentModel();
        UpdateCommentModel updateCommentModel = new UpdateCommentModel();
        if (await IsAuthor(review.Author))
        {
            editReviewModel.AccessMode = AccessMode.Changeable;
        }

        var comments = _commentService.GetCommentsByReviewId(review!.ReviewId);
        var reviewDetailsModel = new ReviewDetailsModel
        {
            CreateCommentModel = createCommentModel, EditReviewModel = editReviewModel,
            Review = review!, Comments = comments, UpdateCommentModel = updateCommentModel
        };
        HttpContext.Session.SetString("reviewId", review.ReviewId.ToString());
        return View(reviewDetailsModel);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Home(int page = 1)
    {
        var reviews = _reviewService.GetReviewBySortModel(new SortModel());
        HomeReviewsModel homeReviewsModel = new HomeReviewsModel {Reviews = reviews};
        return View(homeReviewsModel);
    }

    [HttpGet]
    public async Task<IActionResult> Reviews(int page = 1)
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        var myReviews = _reviewService.GetReviewsByAuthorId(currentUser.Id);
        MyReviewsModel myReviewsModel = new MyReviewsModel
        {
            CreateReviewModel = new CreateReviewModel(), Reviews = myReviews,
            SortModel = new SortModel()
        };

        return View(myReviewsModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(MyReviewsModel myReviewsModel)
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View("Reviews");
        }

        var author = await _userManager.GetUserAsync(HttpContext.User);
        await _reviewService.CreateReview(myReviewsModel.CreateReviewModel, author);
        return RedirectToAction("Reviews", "Reviews");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeReviewStatus(string status, int reviewId)
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        await _reviewService.ChangeReviewStatus((Status) Enum.Parse(typeof(Status), status, true), reviewId);
        return RedirectToAction("Reviews");
    }

    private async Task<bool> IsAuthor(User user)
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        return currentUser != null && currentUser.Id.Equals(user.Id);
    }
}