using GakkoBackend.Application.Account.Commands.AddRefreshToken;
using GakkoBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GakkoBackend.Application.Account.Queries.CheckRefreshToken
{
    public class RefreshTokenQuery : IRequest<AddRefreshTokenCommand>
    {
        public string Name { get; set; }
        public string RefreshToken { get; set; }

        public class Handler : IRequestHandler<RefreshTokenQuery, AddRefreshTokenCommand>
        {
            private readonly StudentAppDbContext _context;
            private readonly IMediator _mediator;

            public Handler(StudentAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<AddRefreshTokenCommand> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var userFromDb =
                  await _context.Person
                      .SingleOrDefaultAsync(x => x.Name == request.Name && x.RefreshToken == request.RefreshToken, cancellationToken);

                return userFromDb == null ? null : new AddRefreshTokenCommand { User = userFromDb };
            }
        }
    }
}
