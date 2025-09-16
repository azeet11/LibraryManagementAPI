using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.Dtos;

namespace LibraryManagementSystem.Services;

public class MemberService
{
    private readonly LibraryDbContext _context;

    public MemberService(LibraryDbContext context)
    {
        _context = context;
    }

    public void AddMember(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();
    }

    public IEnumerable<MemberDto> GetAllMembers()
    {
        return _context.Members
            .Select(m => new MemberDto
            {
                Id = m.Id,
                Name = m.Name
            })
            .ToList();
    }

    public Member? GetMemberById(int id)
    {
        return _context.Members.Find(id);
    }
}
