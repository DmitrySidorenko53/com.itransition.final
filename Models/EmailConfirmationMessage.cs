namespace com.itransition.final.Models;

public class EmailConfirmationMessage
{
    public string EmailTo { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
}