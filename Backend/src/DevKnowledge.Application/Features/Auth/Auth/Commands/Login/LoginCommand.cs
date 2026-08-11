using DevKnowledge.Application.Common.Models;
using MediatR;

namespace DevKnowledge.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthResult>;
