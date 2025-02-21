using Microsoft.AspNetCore.Mvc;
using MvcUnitTesting_dotnet8.Models;
using System.Diagnostics;
using Tracker.WebAPIClient;

namespace MvcUnitTesting_dotnet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Book> _repository;

        // Inject the repository and logger
        public HomeController(IRepository<Book> bookRepo, ILogger<HomeController> logger)
        {
            _repository = bookRepo ?? throw new ArgumentNullException(nameof(bookRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Track the activity
            ActivityAPIClient.Track(
                StudentID: "S00236260",
                StudentName: "James Mccafferty Devers",
                activityName: "Rad302 2025 Week 2 Lab 1",
                Task: "Running initial tests"
            );

            _logger.LogInformation("HomeController initialized.");
        }

        // Display the list of books
        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await _repository.GetAllAsync();
                return View(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving books");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // Display Privacy page
        public IActionResult Privacy()
        {
            ViewData["Message"] = "Your Privacy is our concern";
            return View();
        }

        // Handle errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
