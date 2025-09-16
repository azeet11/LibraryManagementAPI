using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Dtos;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;
    private readonly MemberService _memberService;

    public BooksController(BookService bookService, MemberService memberService)
    {
        _bookService = bookService;
        _memberService = memberService;
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] BookDto bookDto)
    {
        var book = new Book { Title = bookDto.Title };
        _bookService.AddBook(book);
        return Ok("Book added successfully!");
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        return Ok(_bookService.GetAllBooks());
    }

    [HttpPost("{bookId}/borrow/{memberId}")]
    public IActionResult BorrowBook(int bookId, int memberId)
    {
        var book = _bookService.GetAllBooks()
            .Select(b => new Book { Id = b.Id, Title = b.Title, IsBorrowed = b.IsBorrowed })
            .FirstOrDefault(b => b.Id == bookId);

        var member = _memberService.GetAllMembers()
            .Select(m => new Member { Id = m.Id, Name = m.Name })
            .FirstOrDefault(m => m.Id == memberId);

        if (book == null || member == null) return NotFound("Book or Member not found");

        var success = _bookService.Borrow(book, member);
        if (!success) return BadRequest("Book is already borrowed");

        return Ok($"{member.Name} borrowed '{book.Title}' successfully!");
    }

    [HttpPost("{bookId}/return/{memberId}")]
    public IActionResult ReturnBook(int bookId, int memberId)
    {
        var book = _bookService.GetAllBooks()
            .Select(b => new Book { Id = b.Id, Title = b.Title, IsBorrowed = b.IsBorrowed })
            .FirstOrDefault(b => b.Id == bookId);

        var member = _memberService.GetAllMembers()
            .Select(m => new Member { Id = m.Id, Name = m.Name })
            .FirstOrDefault(m => m.Id == memberId);

        if (book == null || member == null) return NotFound("Book or Member not found");

        var success = _bookService.Return(book, member);
        if (!success) return BadRequest("Book was not borrowed");

        return Ok($"{member.Name} returned '{book.Title}' successfully!");
    }

    [HttpGet("loans")]
    public IActionResult GetAllLoans()
    {
        return Ok(_bookService.GetLoans());
    }
}
