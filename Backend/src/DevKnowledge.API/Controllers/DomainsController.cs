using DevKnowledge.Application.Features.Domains.Commands.CreateDomain;
using DevKnowledge.Application.Features.Domains.Queries.GetDomains;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevKnowledge.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DomainsController : ControllerBase
{
    private readonly ISender _sender;

    public DomainsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize] // Yêu cầu đăng nhập, theo rule đã chốt
    public async Task<IActionResult> GetDomains()
    {
        var result = await _sender.Send(new GetDomainsQuery());
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới được tạo
    public async Task<IActionResult> CreateDomain([FromBody] CreateDomainCommand command)
    {
        var domainId = await _sender.Send(command);
        return Created($"/api/v1/domains/{domainId}", new { id = domainId });
    }
}
