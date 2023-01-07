using com.itransition.final.Services;
using com.itransition.final.ViewModels.Reviews.ReviewDetails;
using Microsoft.AspNetCore.Mvc;

namespace com.itransition.final.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public Task<IActionResult> CreateComment(CommentModel commentModel)
    {
        var comment = _commentService.CreateComment(commentModel);
        
    }
}