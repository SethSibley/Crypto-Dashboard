namespace CryptoSignalAIDashboard.Models;

public class DashboardViewModel
{
    public int TotalPostsAnalyzed { get; set; }
    public string OverallSentiment { get; set; } = string.Empty;
    public CryptoTopic? TopTrendingTopic { get; set; }
    public CryptoTopic? HighestMomentumTopic { get; set; }
    public List<CryptoTopic> Topics { get; set; } = new();
}
