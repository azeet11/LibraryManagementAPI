using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _context.Books.Include(b => b.Loans).ToList();
    }

    public Book? GetBookById(int id)
    {
        return _context.Books.Include(b => b.Loans).FirstOrDefault(b => b.Id == id);
    }

    public void UpdateBook(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
    }
}
