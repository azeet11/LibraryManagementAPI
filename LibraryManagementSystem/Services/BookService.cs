using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class BookService : IBorrowable
{
    private readonly IBookRepository _bookRepository;
    private readonly INotificationService _notificationService;

    // Track loans (in-memory)
    private readonly List<Loan> _loans = new();

    public BookService(IBookRepository bookRepository, INotificationService notificationService)
    {
        _bookRepository = bookRepository;
        _notificationService = notificationService;
    }

    public void AddBook(Book book)
    {
        _bookRepository.AddBook(book);
        _notificationService.Notify($"Book '{book.Title}' added!");
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _bookRepository.GetAllBooks();
    }

    public bool Borrow(Book book, Member member)
    {
        if (book.IsBorrowed)
            return false;

        book.IsBorrowed = true;
        _bookRepository.UpdateBook(book);

        _loans.Add(new Loan
        {
            BookId = book.Id,
            MemberId = member.Id,
            BorrowedDate = DateTime.Now
        });

        _notificationService.Notify($"{member.Name} borrowed '{book.Title}'");
        return true;
    }

    public bool Return(Book book, Member member)
    {
        if (!book.IsBorrowed)
            return false;

        book.IsBorrowed = false;
        _bookRepository.UpdateBook(book);

        var loan = _loans.FirstOrDefault(l => l.BookId == book.Id && l.MemberId == member.Id && l.ReturnedDate == null);
        if (loan != null)
        {
            loan.ReturnedDate = DateTime.Now;
        }

        _notificationService.Notify($"{member.Name} returned '{book.Title}'");
        return true;
    }

    // Optional: Get all loans
    public IEnumerable<Loan> GetLoans()
    {
        return _loans;
    }
}
