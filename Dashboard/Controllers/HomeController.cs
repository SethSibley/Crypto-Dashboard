using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CryptoSignalAIDashboard.Models;
using CryptoSignalAIDashboard.Services;

namespace CryptoSignalAIDashboard.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICryptoSignalService _cryptoSignalService;

    public HomeController(ILogger<HomeController> logger, ICryptoSignalService cryptoSignalService)
    {
        _logger = logger;
        _cryptoSignalService = cryptoSignalService;
    }

    public IActionResult Index()
    {
        var dashboard = _cryptoSignalService.GetDashboard();
        return View(dashboard);
    }

    public IActionResult Details(int id)
    {
        var topic = _cryptoSignalService.GetTopicById(id);

        if (topic == null)
        {
            return NotFound();
        }

        return View(topic);
    }

    public IActionResult DailyBrief()
    {
        var dailyBrief = _cryptoSignalService.GetDailyBrief();
        return View(dailyBrief);
    }

    [HttpGet]
    public IActionResult ContentGenerator()
    {
        var model = new ContentGeneratorViewModel
        {
            Topics = _cryptoSignalService.GetTopics()
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult ContentGenerator(int topicId)
    {
        var model = new ContentGeneratorViewModel
        {
            SelectedTopicId = topicId,
            Topics = _cryptoSignalService.GetTopics(),
            GeneratedContent = _cryptoSignalService.GenerateContent(topicId)
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
