using com.itransition.final.Models.UserData;

namespace com.itransition.final.Models.ReviewModels;

public class ReviewRating
{
    public int ReviewRatingId { get; set; }
        
    
    public User User { get; set; } = null!;
    
    public Review Review { get; set; } = null!;

    public int Rate { get; set; }
    public bool IsLiked { get; set; }
}