using DevKnowledge.Application.Common.Exceptions;
using DevKnowledge.Application.Features.Domains.Commands.CreateDomain;
using DevKnowledge.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Domain.Common;
using Xunit;

namespace DevKnowledge.UnitTests.Application.Features.Domains.Commands;

public class CreateDomainCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly CreateDomainCommandHandler _handler;

    public CreateDomainCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _handler = new CreateDomainCommandHandler(_contextMock.Object);
    }

    private T SetId<T>(T entity, Guid id) where T : BaseEntity
    {
        typeof(BaseEntity).GetProperty("Id")?.SetValue(entity, id);
        return entity;
    }

    [Fact]
    public async Task Handle_ShouldCreateDomain_WhenNameAndSlugAreUnique()
    {
        // Arrange
        var command = new CreateDomainCommand("Backend", "Backend development", "backend");
        var emptyDomains = new List<Domain.Entities.Domain>();

        _contextMock.Setup(c => c.Domains).ReturnsDbSet(emptyDomains);
        _contextMock.Setup(c => c.Domains.Add(It.IsAny<Domain.Entities.Domain>()))
            .Callback<Domain.Entities.Domain>(d => SetId(d, Guid.NewGuid()));
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _contextMock.Verify(c => c.Domains.Add(It.IsAny<Domain.Entities.Domain>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenNameOrSlugAlreadyExists()
    {
        // Arrange
        var command = new CreateDomainCommand("Backend", "Backend development", "backend");
        
        var existingDomains = new List<Domain.Entities.Domain>
        {
            new Domain.Entities.Domain { Name = "Backend", Slug = "backend-slug" }
        };

        _contextMock.Setup(c => c.Domains).ReturnsDbSet(existingDomains);

        // Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<ValidationException>();
        _contextMock.Verify(c => c.Domains.Add(It.IsAny<Domain.Entities.Domain>()), Times.Never);
    }
}
