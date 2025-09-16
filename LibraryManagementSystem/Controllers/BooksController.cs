using LibraryManagementSystem.Models;
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
    public IActionResult AddBook([FromBody] Book book)
    {
        _bookService.AddBook(book);
        return Ok("Book added successfully!");
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        return Ok(_bookService.GetAllBooks());
    }

    // Borrow a book
    [HttpPost("{bookId}/borrow/{memberId}")]
    public IActionResult BorrowBook(int bookId, int memberId)
    {
        var book = _bookService.GetAllBooks().FirstOrDefault(b => b.Id == bookId);
        var member = _memberService.GetAllMembers().FirstOrDefault(m => m.Id == memberId);

        if (book == null || member == null)
            return NotFound("Book or Member not found");

        var success = _bookService.Borrow(book, member);
        if (!success)
            return BadRequest("Book is already borrowed");

        return Ok($"{member.Name} borrowed '{book.Title}' successfully!");
    }

    // Return a book
    [HttpPost("{bookId}/return/{memberId}")]
    public IActionResult ReturnBook(int bookId, int memberId)
    {
        var book = _bookService.GetAllBooks().FirstOrDefault(b => b.Id == bookId);
        var member = _memberService.GetAllMembers().FirstOrDefault(m => m.Id == memberId);

        if (book == null || member == null)
            return NotFound("Book or Member not found");

        var success = _bookService.Return(book, member);
        if (!success)
            return BadRequest("Book was not borrowed");

        return Ok($"{member.Name} returned '{book.Title}' successfully!");
    }

    [HttpGet("loans")]
    public IActionResult GetAllLoans()
    {
        var loans = _bookService.GetLoans();
        return Ok(loans);
    }
}