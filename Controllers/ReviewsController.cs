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

    public ReviewsController(IReviewService reviewService, UserManager<User> userManager)
    {
        _reviewService = reviewService;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult ReviewDetails(int reviewId)
    {
        var review = _reviewService.GetReviewById(reviewId);
        var editReviewModel = new EditReviewModel();
        var commentModel = new CommentModel();
        var reviewDetailsModel = new ReviewDetailsModel
        {
            CommentModel = commentModel, EditReviewModel =
                editReviewModel,
            Review = review!
        };
        return View(reviewDetailsModel);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Home(int page = 1)
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Reviews(int page = 1)
    {
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
        await _reviewService.ChangeReviewStatus((Status) Enum.Parse(typeof(Status), status, true), reviewId);
        return RedirectToAction("Reviews");
    }
}