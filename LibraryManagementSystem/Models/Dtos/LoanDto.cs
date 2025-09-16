namespace LibraryManagementSystem.Models.Dtos;

public class LoanDto
{
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;

    public int MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;

    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}
