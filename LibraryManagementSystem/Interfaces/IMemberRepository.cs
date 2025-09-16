using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Interfaces;

public interface IMemberRepository
{
    void AddMember(Member member);
    IEnumerable<Member> GetAllMembers();
    Member? GetMemberById(int id);
}
