using DevKnowledge.Application.Common.Exceptions;
using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Infrastructure.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace DevKnowledge.UnitTests.Infrastructure.Identity;

public class IdentityServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly IdentityService _identityService;

    public IdentityServiceTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        
        _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        _dbContextMock = new Mock<IApplicationDbContext>();

        var jwtOptions = Options.Create(new JwtSettings 
        { 
            Issuer = "Test", 
            Audience = "Test", 
            Secret = "SuperSecretKeyForTestingPurposeOnly",
            AccessTokenExpiryMinutes = 15,
            RefreshTokenExpiryDays = 7
        });

        _identityService = new IdentityService(
            _userManagerMock.Object,
            _jwtTokenGeneratorMock.Object,
            _dbContextMock.Object,
            jwtOptions);
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrowValidationException_WhenEmailAlreadyExists()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new ApplicationUser()); // User exists

        // Act
        var act = async () => await _identityService.RegisterAsync("test@test.com", "Password", "Test User");

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorized_WhenUserNotFound()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((ApplicationUser)null); // User not found

        // Act
        var act = async () => await _identityService.LoginAsync("wrong@test.com", "Password");

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorized_WhenPasswordIsWrong()
    {
        // Arrange
        var user = new ApplicationUser();
        _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>()))
            .ReturnsAsync(false); // Wrong password

        // Act
        var act = async () => await _identityService.LoginAsync("test@test.com", "WrongPassword");

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
