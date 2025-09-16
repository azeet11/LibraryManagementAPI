using LibraryManagementSystem.Data;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;

    public MemberRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public void AddMember(Member member)
    {
        _context.Members.Add(member);
        _context.SaveChanges();
    }

    public IEnumerable<Member> GetAllMembers()
    {
        return _context.Members.Include(m => m.Loans).ToList();
    }

    public Member? GetMemberById(int id)
    {
        return _context.Members.Include(m => m.Loans).FirstOrDefault(m => m.Id == id);
    }
}
