using com.itransition.final.Models.ReviewModels;

namespace com.itransition.final.ViewModels.Reviews;

public class SortModel
{
    public Category CategoryBy { get; set; }
    public Sort SortBy { get; set; }
    public Order OrderBy { get; set; }
    public DateTime StartFromDate { get; set; }
}