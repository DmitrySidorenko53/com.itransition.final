using com.itransition.final.Models.UserData;

namespace com.itransition.final.Models.ReviewModels;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime PublishDateTime { get; set; }
    public Status Status;
    
    public User Author { get; set; } = null!;

    public Review Review { get; set; } = null!;
    
}