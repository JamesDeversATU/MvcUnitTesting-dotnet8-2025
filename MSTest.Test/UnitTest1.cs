using Microsoft.AspNetCore.Mvc;
using MvcUnitTesting_dotnet8.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcUnitTesting_dotnet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Book> _bookRepository;

        public HomeController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index(string genre)
        {
            var books = _bookRepository.GetAll();

            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre == genre).ToList();
                ViewData["Genre"] = genre;
            }

            return View(books);
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = "Your Privacy is our concern";
            return View();
        }
    }
}
