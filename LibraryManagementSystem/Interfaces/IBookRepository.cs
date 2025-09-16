using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces;

public interface IBookRepository
{
    void AddBook(Book book);
    IEnumerable<Book> GetAllBooks();
    Book? GetBookById(int id);
    void UpdateBook(Book book);
}
