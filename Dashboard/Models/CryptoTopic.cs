namespace CryptoSignalAIDashboard.Models;

public class CryptoTopic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Mentions { get; set; }
    public string Sentiment { get; set; } = string.Empty;
    public string Momentum { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public List<SourcePost> SourcePosts { get; set; } = new();
}
