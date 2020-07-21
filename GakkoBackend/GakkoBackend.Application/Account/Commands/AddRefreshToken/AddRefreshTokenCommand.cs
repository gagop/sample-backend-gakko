using MediatR;
using System;
using GakkoBackend.Shared.Constants;
using System.Threading.Tasks;
using System.Threading;
using GakkoBackend.Persistence;
using GakkoBackend.Domain;
using Microsoft.EntityFrameworkCore;

namespace GakkoBackend.Application.Account.Commands.AddRefreshToken
{
    public class AddRefreshTokenCommand : IRequest<string>
    {
        public Person Person { get; set; }
        public Employee Employee { get; set; }
        public class Handler : IRequestHandler<AddRefreshTokenCommand, string>
        {
            private readonly GakkoBackendContext _context;
            private readonly IMediator _mediator;

            public Handler(GakkoBackendContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<string> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var refreshToken = Helpers.GenerateRefreshToken();

                request.Employee.RefreshToken = refreshToken;
                request.Employee.RefreshTokenExpDate = DateTime.UtcNow.AddHours(GlobalConsts.REFRESH_TOKEN_EXP_TIME_IN_HOURS);

                await _context.SaveChangesAsync(cancellationToken);

                return refreshToken;
            }
        }
    }
}
