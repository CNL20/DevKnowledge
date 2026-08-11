using DevKnowledge.Application.Common.Models;
using MediatR;

namespace DevKnowledge.Application.Auth.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string DisplayName
) : IRequest<AuthResult>;
