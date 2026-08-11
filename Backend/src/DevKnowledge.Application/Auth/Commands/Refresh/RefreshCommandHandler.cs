using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Application.Common.Models;
using MediatR;

namespace DevKnowledge.Application.Auth.Commands.Refresh;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, AuthResult>
{
    private readonly IIdentityService _identityService;

    public RefreshCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthResult> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RefreshAsync(request.RefreshToken, cancellationToken);
    }
}
