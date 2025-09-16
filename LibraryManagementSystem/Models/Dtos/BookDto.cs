namespace LibraryManagementSystem.Models.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsBorrowed { get; set; }
}
