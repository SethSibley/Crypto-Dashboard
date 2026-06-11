using CryptoSignalAIDashboard.Models;

namespace CryptoSignalAIDashboard.Services;

public interface ICryptoSignalService
{
    List<CryptoTopic> GetTopics();
    CryptoTopic? GetTopicById(int id);
    DashboardViewModel GetDashboard();
    DailyBrief GetDailyBrief();
    GeneratedContent GenerateContent(int topicId);
}
