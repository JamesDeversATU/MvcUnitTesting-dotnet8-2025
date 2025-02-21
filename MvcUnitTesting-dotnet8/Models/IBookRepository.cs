using System.Collections.Generic;
using System.Threading.Tasks;
using MvcUnitTesting_dotnet8.Models;

namespace MvcUnitTesting_dotnet8.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<bool> BookExistsAsync(int id);
    }
}