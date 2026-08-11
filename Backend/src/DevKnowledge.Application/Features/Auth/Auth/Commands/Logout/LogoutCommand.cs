using MediatR;

namespace DevKnowledge.Application.Features.Auth.Commands.Logout;

public record LogoutCommand(
    string RefreshToken
) : IRequest;
