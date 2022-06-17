namespace TestApp.Models
{
    public class EmailMessage
    {
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
