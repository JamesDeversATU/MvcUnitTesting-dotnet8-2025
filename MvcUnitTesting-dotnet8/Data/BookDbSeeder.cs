using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Models;

namespace MvcUnitTesting_dotnet8.Data
{
    public class BookDbSeeder
    {
        private readonly BookDbContext _context;

        public BookDbSeeder(BookDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Seed data only if no books exist
            if (!_context.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m },
                    new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m },
                    new Book { Genre="Science Fiction", ID=3, Name="Escape from the Vortex", Price=12.50m },
                    new Book { Genre="History", ID=4, Name="The Battle of the Somme", Price=22m }
                };

                _context.Books.AddRange(books);
                _context.SaveChanges();
            }
        }
    }
}
