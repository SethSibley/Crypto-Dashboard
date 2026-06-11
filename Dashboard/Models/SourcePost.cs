namespace CryptoSignalAIDashboard.Models;

public class SourcePost
{
    public int Id { get; set; }
    public string SourceName { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Sentiment { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
}
