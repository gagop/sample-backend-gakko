using GakkoBackend.Entities;
using GakkoBackend.Persistence;
using MediatR;
using System;
using GakkoBackend.Shared.Constants;
using System.Threading.Tasks;
using System.Threading;

namespace GakkoBackend.Application.Account.Commands.AddRefreshToken
{
    public class AddRefreshTokenCommand : IRequest<string>
    {
        public Person User { get; set; }
        public class Handler : IRequestHandler<AddRefreshTokenCommand, string>
        {
            private readonly StudentAppDbContext _context;
            private readonly IMediator _mediator;

            public Handler(StudentAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<string> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var refreshToken = Helpers.GenerateRefreshToken();

                request.User.RefreshToken = refreshToken;
                request.User.RefreshTokenExp = DateTime.UtcNow.AddHours(GlobalConsts.REFRESH_TOKEN_EXP_TIME_IN_HOURS);

                await _context.SaveChangesAsync(cancellationToken);

                return refreshToken;
            }
        }
    }
}
