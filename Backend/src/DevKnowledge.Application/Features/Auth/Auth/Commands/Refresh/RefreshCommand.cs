using DevKnowledge.Application.Common.Models;
using MediatR;

namespace DevKnowledge.Application.Features.Auth.Commands.Refresh;

public record RefreshCommand(
    string RefreshToken
) : IRequest<AuthResult>;
