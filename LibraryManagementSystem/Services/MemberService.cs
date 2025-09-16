using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class MemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly INotificationService _notificationService;

    public MemberService(IMemberRepository memberRepository, INotificationService notificationService)
    {
        _memberRepository = memberRepository;
        _notificationService = notificationService;
    }

    public void AddMember(Member member)
    {
        _memberRepository.AddMember(member);
        _notificationService.Notify($"Member '{member.Name}' added!");
    }

    public IEnumerable<Member> GetAllMembers()
    {
        return _memberRepository.GetAllMembers();
    }
}