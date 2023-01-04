using com.itransition.final.Models.UserData;

namespace com.itransition.final.Models.ReviewModels;

public class Review
{
    public int ReviewId { get; set; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public int ProductRating { get; set; }
    public DateTime PublishDateTime { get; set; }
    public Status Status { get; set; }
    public byte[] Image { get; set; } = null!;

    public User Author { get; set; } = null!;

    public Product Product { get; set; } = null!;

    public List<Comment> Comments { get; set; } = null!;
    public List<ReviewRating> ReviewRatings { get; set; } = null!;
}