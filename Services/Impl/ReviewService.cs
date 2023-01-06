using com.itransition.final.Models;
using com.itransition.final.Models.Context;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using com.itransition.final.ViewModels.Reviews;

namespace com.itransition.final.Services.Impl;

public class ReviewService : IReviewService
{
    private readonly ApplicationContext _context;

    public ReviewService(ApplicationContext applicationContext)
    {
        _context = applicationContext;
    }

    public List<Review> GetReviewsByUserId(string userId)
    {
        return _context.Reviews.Where(r => r.Author.Id == userId).ToList();
    }

    public List<Review> GetReviewBySortModel(SortModel sortModel)
    {
        
    }

    public Review? GetReviewsById(int reviewId)
    {
        return _context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
    }

    
    public async Task CreateReview(ReviewModel reviewModel, User author)
    {
        var review = CreateFromModel(reviewModel, author);
        var product = CreateFromModel(reviewModel, review);
        await _context.Reviews.AddAsync(review);
        if (_context.Products.FirstOrDefault(r => r.Title == reviewModel.ProductTitle) == null)
        {
            await _context.Products.AddAsync(product);
        }

        review.Product = product;
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    private Review CreateFromModel(ReviewModel reviewModel, User author)
    {
        return new Review
        {
            Title = reviewModel.Title,
            Text = reviewModel.Text,
            Status = Status.Visible,
            PublishDateTime = DateTime.Now,
            Author = author,
            Comments = new List<Comment>(),
            ReviewRatings = new List<ReviewRating>()
        };
    }

    private Product CreateFromModel(ReviewModel reviewModel, Review review)
    {
        return new Product
        {
            Title = reviewModel.ProductTitle,
            Category = reviewModel.Category,
            Reviews = new List<Review>() {review}
        };
    }
}