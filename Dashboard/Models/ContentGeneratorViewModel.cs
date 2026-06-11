namespace CryptoSignalAIDashboard.Models;

public class ContentGeneratorViewModel
{
    public int? SelectedTopicId { get; set; }
    public List<CryptoTopic> Topics { get; set; } = new();
    public GeneratedContent? GeneratedContent { get; set; }
}
