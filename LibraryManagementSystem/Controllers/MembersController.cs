using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly MemberService _memberService;

    public MembersController(MemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public IActionResult AddMember([FromBody] Member member)
    {
        _memberService.AddMember(member);
        return Ok("Member added successfully!");
    }

    [HttpGet]
    public IActionResult GetAllMembers()
    {
        return Ok(_memberService.GetAllMembers());
    }
}
