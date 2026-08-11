using DevKnowledge.Application.Common.Exceptions;
using DevKnowledge.Application.Features.Topics.Commands.CreateTopic;
using DevKnowledge.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Domain.Common;
using Xunit;

namespace DevKnowledge.UnitTests.Application.Features.Topics.Commands;

public class CreateTopicCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly CreateTopicCommandHandler _handler;

    public CreateTopicCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _handler = new CreateTopicCommandHandler(_contextMock.Object);
    }

    private T SetId<T>(T entity, Guid id) where T : BaseEntity
    {
        typeof(BaseEntity).GetProperty("Id")?.SetValue(entity, id);
        return entity;
    }

    [Fact]
    public async Task Handle_ShouldCreateTopic_WhenDomainExistsAndNameAndSlugAreUnique()
    {
        // Arrange
        var domainId = Guid.NewGuid();
        var command = new CreateTopicCommand(domainId, "C#", "C# programming", "csharp");
        
        var domain = SetId(new Domain.Entities.Domain { Name = "Backend", Slug = "backend" }, domainId);
        var domains = new List<Domain.Entities.Domain> { domain };
        var topics = new List<Topic>();

        _contextMock.Setup(c => c.Domains).ReturnsDbSet(domains);
        _contextMock.Setup(c => c.Topics).ReturnsDbSet(topics);
        _contextMock.Setup(c => c.Topics.Add(It.IsAny<Topic>()))
            .Callback<Topic>(t => SetId(t, Guid.NewGuid()));
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _contextMock.Verify(c => c.Topics.Add(It.IsAny<Topic>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenDomainDoesNotExist()
    {
        // Arrange
        var domainId = Guid.NewGuid();
        var command = new CreateTopicCommand(domainId, "C#", "C# programming", "csharp");
        
        var domains = new List<Domain.Entities.Domain>(); // Empty, domain not found
        _contextMock.Setup(c => c.Domains).ReturnsDbSet(domains);

        // Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<NotFoundException>();
        _contextMock.Verify(c => c.Topics.Add(It.IsAny<Topic>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenNameOrSlugAlreadyExistsInDomain()
    {
        // Arrange
        var domainId = Guid.NewGuid();
        var command = new CreateTopicCommand(domainId, "C#", "C# programming", "csharp");
        
        var domain = SetId(new Domain.Entities.Domain(), domainId);
        var domains = new List<Domain.Entities.Domain> { domain };
        var existingTopics = new List<Topic>
        {
            new Topic { DomainId = domainId, Name = "C#", Slug = "csharp" }
        };

        _contextMock.Setup(c => c.Domains).ReturnsDbSet(domains);
        _contextMock.Setup(c => c.Topics).ReturnsDbSet(existingTopics);

        // Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<ValidationException>();
        _contextMock.Verify(c => c.Topics.Add(It.IsAny<Topic>()), Times.Never);
    }
}
