namespace LibraryManagementSystem.Models;

public class Loan
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowedDate { get; set; } = DateTime.Now;
    public DateTime? ReturnedDate { get; set; }
}
