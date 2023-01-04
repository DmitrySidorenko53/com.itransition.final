namespace com.itransition.final.Models.ReviewModels;

public class Product
{
    public int ProductId { get; set; }
    public string Title { get; set; } = null!;
    public Category Category { get; set; }
    
    public List<Review> Reviews { get; set; } = null!;
}