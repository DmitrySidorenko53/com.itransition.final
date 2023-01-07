using com.itransition.final.Models;
using com.itransition.final.Models.Context;
using com.itransition.final.Models.ReviewModels;
using com.itransition.final.Models.UserData;
using com.itransition.final.ViewModels;
using com.itransition.final.ViewModels.Reviews;
using com.itransition.final.ViewModels.Reviews.MyReviews;
using Microsoft.EntityFrameworkCore;

namespace com.itransition.final.Services.Impl;

public class ReviewService : IReviewService
{
    private readonly ApplicationContext _context;

    public ReviewService(ApplicationContext applicationContext)
    {
        _context = applicationContext;
    }

    public List<Review> GetReviewsByAuthorId(string? userId)
    {
        var myReviews = _context.Reviews.Where(r => r.Author.Id == userId).OrderByDescending(r => r.PublishDateTime)
            .Include(r => r.Product).Where(r => r.Status != Status.Deleted).ToList();
        return myReviews.Count == 0 ? new List<Review>() : myReviews;
    }

    public List<Review> GetReviewBySortModel(SortModel? sortModel)
    {
        if (sortModel == null)
        {
            return _context.Reviews.OrderByDescending(r => r.PublishDateTime)
                .ThenByDescending(r => r.ReviewRatings.Where(rr => rr.Rate != 0).Average(rr => rr.Rate))
                .Where(r => r.Status == Status.Visible).ToList();
        }

        var reviewsByCategory = sortModel.CategoryBy == Category.None
            ? _context.Reviews.OrderByDescending(r => r.PublishDateTime)
                .ThenByDescending(r => r.ReviewRatings.Where(rr => rr.Rate != 0).Average(rr => rr.Rate))
                .Where(r => r.Status == Status.Visible).ToList()
            : _context.Reviews.Where(r => r.Product.Category == sortModel.CategoryBy)
                .Where(r => r.Status == Status.Visible).ToList();
        return FilterReviewsBySortAndOrder(reviewsByCategory, sortModel);
    }

    private List<Review> FilterReviewsBySortAndOrder(List<Review> reviews, SortModel sortModel)
    {
        switch (sortModel.SortBy)
        {
            case Sort.Comments:
                reviews = sortModel.OrderBy == Order.Asc
                    ? reviews.OrderBy(r => r.Comments.Count).Where(r => r.Status != Status.Hidden).ToList()
                    : reviews.OrderByDescending(r => r.Comments.Count).Where(r => r.Status == Status.Visible).ToList();
                break;
            case Sort.Likes:
                reviews = sortModel.OrderBy == Order.Asc
                    ? reviews.OrderBy(r => r.ReviewRatings.Count(rr => rr.IsLiked))
                        .Where(r => r.Status == Status.Visible).ToList()
                    : reviews.OrderByDescending(r => r.ReviewRatings.Count(rr => rr.IsLiked))
                        .Where(r => r.Status == Status.Visible).ToList();
                break;
            case Sort.Date:
                reviews = sortModel.OrderBy == Order.Asc
                    ? reviews.OrderBy(r => r.PublishDateTime).Where(r => r.Status != Status.Hidden).ToList()
                    : reviews.OrderByDescending(r => r.PublishDateTime).Where(r => r.Status == Status.Visible).ToList();
                break;
            case Sort.Rate:
                reviews = sortModel.OrderBy == Order.Asc
                    ? reviews.OrderBy(r => r.ReviewRatings.Where(r => r.Rate != 0).Average(rr => rr.Rate))
                        .Where(r => r.Status == Status.Visible).ToList()
                    : reviews.OrderByDescending(r => r.ReviewRatings.Where(r => r.Rate != 0).Average(rr => rr.Rate))
                        .Where(r => r.Status == Status.Visible).ToList();
                break;
        }

        return reviews;
    }

    public Review? GetReviewById(int reviewId)
    {
        return _context.Reviews.Where(r => r.Status == Status.Visible).Include(r => r.Author)
            .Include(r => r.Comments).Include(r => r.ReviewRatings)
            .Include(r => r.Product)
            .FirstOrDefault(r => r.ReviewId == reviewId);
    }


    public async Task CreateReview(CreateReviewModel? reviewModel, User author)
    {
        var review = CreateFromModel(reviewModel, author);
        var product = CreateFromModel(reviewModel, review);
        await _context.Reviews.AddAsync(review);
        if (_context.Products.FirstOrDefault(r => r.Title == reviewModel!.ProductTitle) == null)
        {
            await _context.Products.AddAsync(product);
        }

        review.Product = product;
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeReviewStatus(Status status, int reviewId)
    {
        var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);
        switch (status)
        {
            case Status.Hidden:
                review!.Status = Status.Hidden;
                break;
            case Status.Visible:
                review!.Status = Status.Hidden;
                break;
            case Status.Deleted:
                review!.Status = Status.Hidden;
                break;
        }

        _context.Reviews.Update(review!);
        await _context.SaveChangesAsync();
    }

    private Review CreateFromModel(CreateReviewModel? reviewModel, User author)
    {
        return new Review
        {
            Title = reviewModel!.Title,
            Text = reviewModel.Text,
            Status = Status.Visible,
            PublishDateTime = DateTime.Now,
            Author = author,
            Comments = new List<Comment>(),
            ReviewRatings = new List<ReviewRating>()
        };
    }

    private Product CreateFromModel(CreateReviewModel? reviewModel, Review review)
    {
        return new Product
        {
            Title = reviewModel!.ProductTitle,
            Category = reviewModel.Category,
            Reviews = new List<Review>() {review}
        };
    }
}