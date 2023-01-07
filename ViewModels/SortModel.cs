using com.itransition.final.Models;
using com.itransition.final.Models.ReviewModels;

namespace com.itransition.final.ViewModels;

public class SortModel
{
    public Category CategoryBy { get; set; } = Category.None;
    public Sort SortBy { get; set; } = Sort.Date;
    public Order OrderBy { get; set; } = Order.Desc;
    public DateTime StartFromDate { get; set; }
}