using DevKnowledge.Application.Auth.Commands.Register;
using DevKnowledge.Application.Auth.Commands.Login;
using FluentAssertions;

namespace DevKnowledge.UnitTests.Application.Auth;

public class AuthCommandValidatorsTests
{
    [Fact]
    public void RegisterCommandValidator_ShouldHaveError_WhenEmailIsInvalid()
    {
        // Arrange
        var validator = new RegisterCommandValidator();
        var command = new RegisterCommand("invalid-email", "Password123!", "Test User");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }

    [Fact]
    public void RegisterCommandValidator_ShouldHaveError_WhenPasswordIsTooShort()
    {
        // Arrange
        var validator = new RegisterCommandValidator();
        var command = new RegisterCommand("test@example.com", "123", "Test User");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterCommandValidator_ShouldNotHaveError_WhenCommandIsValid()
    {
        // Arrange
        var validator = new RegisterCommandValidator();
        var command = new RegisterCommand("test@example.com", "Password123!", "Test User");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
