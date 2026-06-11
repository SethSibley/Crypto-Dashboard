using CryptoSignalAIDashboard.Models;

namespace CryptoSignalAIDashboard.Services;

public class MockCryptoSignalService : ICryptoSignalService
{
    private readonly List<CryptoTopic> _topics;

    public MockCryptoSignalService()
    {
        _topics = BuildMockTopics();
    }

    public List<CryptoTopic> GetTopics()
    {
        return _topics;
    }

    public CryptoTopic? GetTopicById(int id)
    {
        return _topics.FirstOrDefault(topic => topic.Id == id);
    }

    public DashboardViewModel GetDashboard()
    {
        var totalPosts = _topics.Sum(topic => topic.SourcePosts.Count);
        var positiveCount = _topics.Count(topic => topic.Sentiment == "Positive");
        var negativeCount = _topics.Count(topic => topic.Sentiment == "Negative");

        var overallSentiment = positiveCount > negativeCount
            ? "Positive"
            : positiveCount == negativeCount ? "Neutral" : "Negative";

        return new DashboardViewModel
        {
            TotalPostsAnalyzed = totalPosts,
            OverallSentiment = overallSentiment,
            TopTrendingTopic = _topics.OrderByDescending(topic => topic.Mentions).FirstOrDefault(),
            HighestMomentumTopic = GetHighestMomentumTopic(),
            Topics = _topics
        };
    }

    public DailyBrief GetDailyBrief()
    {
        var biggestMomentumTopic = GetHighestMomentumTopic();

        return new DailyBrief
        {
            OverallMarketNarrative = "Crypto discussion is leaning constructive today, with ETF flows and staking yields creating confidence while regulation remains the main source of caution.",
            TopPositiveTopics = _topics
                .Where(topic => topic.Sentiment == "Positive")
                .OrderByDescending(topic => topic.Mentions)
                .Take(3)
                .ToList(),
            TopNegativeTopics = _topics
                .Where(topic => topic.Sentiment == "Negative")
                .OrderByDescending(topic => topic.Mentions)
                .Take(3)
                .ToList(),
            BiggestMomentumTopic = biggestMomentumTopic,
            SocialPostIdeas =
            [
                "Explain why ETF inflows matter for everyday crypto investors.",
                "Compare staking sentiment across Ethereum and Solana communities.",
                "Create a quick market-watch thread on regulation headlines and investor confidence."
            ]
        };
    }

    public GeneratedContent GenerateContent(int topicId)
    {
        var topic = GetTopicById(topicId);

        if (topic == null)
        {
            return new GeneratedContent();
        }

        return new GeneratedContent
        {
            Topic = topic,
            TwitterPost = $"{topic.Name} is getting {topic.Momentum.ToLower()} attention today. Sentiment is {topic.Sentiment.ToLower()} as traders react to: {topic.Summary}",
            InstagramCaption = $"Today's crypto signal: {topic.Name}. The conversation is {topic.Sentiment.ToLower()}, mentions are climbing to {topic.Mentions}, and the key theme is simple: {topic.Summary}",
            LinkedInPost = $"Crypto market intelligence update: {topic.Name} is showing {topic.Momentum.ToLower()} momentum with {topic.Mentions} tracked mentions. Current sentiment is {topic.Sentiment.ToLower()}. The main narrative: {topic.Summary}"
        };
    }

    private CryptoTopic? GetHighestMomentumTopic()
    {
        var momentumScores = new Dictionary<string, int>
        {
            ["Low"] = 1,
            ["Medium"] = 2,
            ["High"] = 3,
            ["Rising"] = 4
        };

        return _topics
            .OrderByDescending(topic => momentumScores.GetValueOrDefault(topic.Momentum, 0))
            .ThenByDescending(topic => topic.Mentions)
            .FirstOrDefault();
    }

    private static List<CryptoTopic> BuildMockTopics()
    {
        return
        [
            new CryptoTopic
            {
                Id = 1,
                Name = "Bitcoin ETF",
                Mentions = 1280,
                Sentiment = "Positive",
                Momentum = "Rising",
                Summary = "ETF inflows are driving optimism as analysts point to stronger institutional demand.",
                SourcePosts =
                [
                    NewPost(1, "X", "MarketWatcher", "Bitcoin ETF inflows are back in focus after another strong trading session.", "Positive", -2),
                    NewPost(2, "Crypto Blog", "ChainDesk", "ETF volume suggests traditional investors are still building exposure.", "Positive", -5),
                    NewPost(3, "Forum", "BTCMacro", "Some traders are waiting for confirmation, but the ETF story still looks strong.", "Neutral", -7)
                ]
            },
            new CryptoTopic
            {
                Id = 2,
                Name = "Ethereum Staking",
                Mentions = 940,
                Sentiment = "Positive",
                Momentum = "High",
                Summary = "Staking rewards and network security are keeping Ethereum discussions constructive.",
                SourcePosts =
                [
                    NewPost(4, "X", "DeFiDaily", "Ethereum staking remains one of the cleaner long-term crypto narratives.", "Positive", -4),
                    NewPost(5, "Newsletter", "Yield Notes", "Validators continue to discuss reward stability and lower volatility.", "Positive", -9)
                ]
            },
            new CryptoTopic
            {
                Id = 3,
                Name = "Solana Network Activity",
                Mentions = 1110,
                Sentiment = "Positive",
                Momentum = "High",
                Summary = "Users are highlighting increased app activity, faster transactions, and growing developer interest.",
                SourcePosts =
                [
                    NewPost(6, "X", "SolanaStats", "Daily active addresses are giving Solana bulls something to talk about.", "Positive", -1),
                    NewPost(7, "Forum", "LayerOneChat", "Developers seem excited, though some users still want better reliability data.", "Neutral", -6)
                ]
            },
            new CryptoTopic
            {
                Id = 4,
                Name = "Crypto Regulation",
                Mentions = 870,
                Sentiment = "Negative",
                Momentum = "Medium",
                Summary = "Regulatory uncertainty is creating caution, especially around exchanges and token listings.",
                SourcePosts =
                [
                    NewPost(8, "News", "Policy Wire", "New comments from regulators renewed questions about exchange compliance.", "Negative", -3),
                    NewPost(9, "X", "LegalCrypto", "Markets dislike uncertainty, and regulation headlines are adding plenty of it.", "Negative", -8)
                ]
            },
            new CryptoTopic
            {
                Id = 5,
                Name = "Coinbase Earnings",
                Mentions = 630,
                Sentiment = "Negative",
                Momentum = "Medium",
                Summary = "Investors are concerned that fee pressure could offset stronger trading activity.",
                SourcePosts =
                [
                    NewPost(10, "Finance Blog", "EquityChain", "Coinbase earnings may offer a cautious read on retail crypto demand.", "Negative", -10),
                    NewPost(11, "X", "ExchangeTracker", "Trading revenue looks better, but fee compression is still the concern.", "Negative", -12)
                ]
            },
            new CryptoTopic
            {
                Id = 6,
                Name = "Bitcoin Mining",
                Mentions = 520,
                Sentiment = "Negative",
                Momentum = "Low",
                Summary = "Mining profitability concerns are weighing on sentiment after higher energy cost discussions.",
                SourcePosts =
                [
                    NewPost(12, "Industry Report", "HashRate Weekly", "Miners are watching margins closely as energy costs stay elevated.", "Negative", -13),
                    NewPost(13, "Forum", "MiningOps", "Hashrate strength is good, but profitability is the issue everyone keeps raising.", "Negative", -16)
                ]
            }
        ];
    }

    private static SourcePost NewPost(int id, string sourceName, string author, string text, string sentiment, int hoursAgo)
    {
        return new SourcePost
        {
            Id = id,
            SourceName = sourceName,
            Author = author,
            Text = text,
            Sentiment = sentiment,
            PublishedAt = DateTime.Today.AddHours(hoursAgo)
        };
    }
}
