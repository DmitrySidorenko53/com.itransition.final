using Microsoft.AspNetCore.Mvc;

namespace com.itransition.final.Controllers;

public class ReviewsController : Controller
{

    [HttpGet]
    public IActionResult Home()
    {
        return View();
    }
}