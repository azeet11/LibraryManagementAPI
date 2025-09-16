using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces;

public interface IBorrowable
{
    bool Borrow(Book book, Member member);
    bool Return(Book book, Member member);
}
