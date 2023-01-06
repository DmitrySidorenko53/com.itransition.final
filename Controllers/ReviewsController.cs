using com.itransition.final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.itransition.final.Controllers;

public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Home()
    {
        return View();
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Reviews()
    {
        return View();
    }
}