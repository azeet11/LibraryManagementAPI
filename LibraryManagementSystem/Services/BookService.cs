using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services;

public class BookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public IEnumerable<BookDto> GetAllBooks()
    {
        return _context.Books
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                IsBorrowed = b.IsBorrowed
            })
            .ToList();
    }

    public bool Borrow(Book book, Member member)
    {
        if (book.IsBorrowed) return false;

        book.IsBorrowed = true;

        var loan = new Loan
        {
            BookId = book.Id,
            MemberId = member.Id,
            BorrowedDate = DateTime.Now
        };

        _context.Loans.Add(loan);
        _context.SaveChanges();

        return true;
    }

    public bool Return(Book book, Member member)
    {
        if (!book.IsBorrowed) return false;

        book.IsBorrowed = false;

        var loan = _context.Loans
            .FirstOrDefault(l => l.BookId == book.Id && l.MemberId == member.Id && l.ReturnedDate == null);

        if (loan != null)
        {
            loan.ReturnedDate = DateTime.Now;
        }

        _context.SaveChanges();
        return true;
    }

    public IEnumerable<LoanDto> GetLoans()
    {
        return _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .Select(l => new LoanDto
            {
                BookId = l.BookId,
                BookTitle = l.Book.Title,
                MemberId = l.MemberId,
                MemberName = l.Member.Name,
                BorrowedDate = l.BorrowedDate,
                ReturnedDate = l.ReturnedDate
            })
            .ToList();
    }
}
