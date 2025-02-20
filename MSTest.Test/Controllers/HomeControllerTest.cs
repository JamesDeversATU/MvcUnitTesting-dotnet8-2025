using Microsoft.AspNetCore.Mvc;
using MvcUnitTesting_dotnet8.Controllers;
using MvcUnitTesting_dotnet8.Models;
using Telerik.JustMock;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcUnitTesting.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Returns_All_Books_In_DB()
        {
            // Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            Mock.Arrange(() => bookRepository.GetAll()).Returns(new List<Book>
            {
                new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                new Book { Genre="Science Fiction", ID=3, Name="Escape from the Vortex", Price=12.50m},
                new Book { Genre="History", ID=4, Name="The Battle of the Somme", Price=22m},
            }).MustBeCalled();

            // Act
            HomeController controller = new HomeController(bookRepository);
            ViewResult viewResult = controller.Index(null) as ViewResult;
            var model = viewResult.Model as IEnumerable<Book>;

            // Assert
            Assert.AreEqual(4, model.Count());
        }

        [TestMethod]
        public void show_ViewData_genre_test()
        {
            // Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            Mock.Arrange(() => bookRepository.GetAll()).Returns(new List<Book>
            {
                new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                new Book { Genre="Science Fiction", ID=3, Name="Escape from the Vortex", Price=12.50m},
                new Book { Genre="History", ID=4, Name="The Battle of the Somme", Price=22m},
            }).MustBeCalled();

            HomeController controller = new HomeController(bookRepository);

            // Act
            string genre = "Fiction";
            ViewResult result = controller.Index(genre) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(genre, result.ViewData["Genre"]);
        }

        [TestMethod]
        public void Privacy()
        {
            // Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            HomeController controller = new HomeController(bookRepository);

            // Act
            ViewResult result = controller.Privacy() as ViewResult;

            // Assert
            Assert.AreEqual("Your Privacy is our concern", result.ViewData["Message"]);
        }
    }
}
