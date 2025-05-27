using Application.Common.Interfaces.Email;
using ErrorOr;
using MediatR;

namespace Application.Users.RecoverPassword;

public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, ErrorOr<Unit>>
{
    private readonly IEmailService _emailService;

    public RecoverPasswordCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<ErrorOr<Unit>> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(request.Email, "Subject test", "<h1>Email test</h1><p>Funciona!</p>");

        return Unit.Value;
    }
}
