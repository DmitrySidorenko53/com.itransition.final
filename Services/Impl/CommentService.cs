using com.itransition.final.Models;
using com.itransition.final.Models.Context;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.ViewModels.Reviews.ReviewDetails;

namespace com.itransition.final.Services.Impl;

public class CommentService : ICommentService
{
    private readonly ApplicationContext _context;

    public CommentService(ApplicationContext context)
    {
        _context = context;
    }

    public List<Comment> GetCommentsByReviewId(int reviewId)
    {
        return _context.Comments.Where(c => c.Review.ReviewId == reviewId).OrderByDescending(c => c.PublishDateTime)
            .Where(c => c.Status == Status.Visible).ToList();
    }

    public async Task CreateComment(CommentModel commentModel)
    {
        var comment = CreateFromModel(commentModel);
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    private Comment CreateFromModel(CommentModel commentModel)
    {
        return new Comment
        {
            Text = commentModel.Text,
            Author = commentModel.Author,
            Review = commentModel.Review,
            Status = Status.Visible,
            PublishDateTime = DateTime.Now
        };
    }

    public async Task UpdateComment(CommentModel commentModel, int commentId)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.CommentId == commentId);
        comment!.Text = commentModel.Text;
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeStatus(Status status, int commentId)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.CommentId == commentId);
        switch (status)
        {
            case Status.Visible : comment!.Status = Status.Visible;
                break;
            case Status.Hidden : comment!.Status = Status.Hidden;
                break;
            case Status.Deleted : comment!.Status = Status.Deleted;
                break;
        }

        _context.Comments.Update(comment!);
        await _context.SaveChangesAsync();
    }
}