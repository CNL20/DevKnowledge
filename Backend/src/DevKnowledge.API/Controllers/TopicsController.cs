using DevKnowledge.Application.Features.Topics.Commands.CreateTopic;
using DevKnowledge.Application.Features.Topics.Queries.GetTopicsByDomain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevKnowledge.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly ISender _sender;

    public TopicsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/api/v1/domains/{domainId:guid}/topics")]
    [Authorize] // Yêu cầu đăng nhập, theo rule đã chốt
    public async Task<IActionResult> GetTopicsByDomain(Guid domainId)
    {
        var result = await _sender.Send(new GetTopicsByDomainQuery(domainId));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới được tạo
    public async Task<IActionResult> CreateTopic([FromBody] CreateTopicCommand command)
    {
        var topicId = await _sender.Send(command);
        return Created($"/api/v1/topics/{topicId}", new { id = topicId });
    }
}
