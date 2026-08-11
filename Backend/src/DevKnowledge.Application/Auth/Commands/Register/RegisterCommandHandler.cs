using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Application.Common.Models;
using MediatR;

namespace DevKnowledge.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RegisterAsync(request.Email, request.Password, request.DisplayName, cancellationToken);
    }
}
