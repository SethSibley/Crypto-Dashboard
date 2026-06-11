namespace CryptoSignalAIDashboard.Models;

public class DailyBrief
{
    public string OverallMarketNarrative { get; set; } = string.Empty;
    public List<CryptoTopic> TopPositiveTopics { get; set; } = new();
    public List<CryptoTopic> TopNegativeTopics { get; set; } = new();
    public CryptoTopic? BiggestMomentumTopic { get; set; }
    public List<string> SocialPostIdeas { get; set; } = new();
}
