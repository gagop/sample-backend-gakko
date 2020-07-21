using GakkoBackend.Application.Account.Commands.AddRefreshToken;
using GakkoBackend.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GakkoBackend.Application.Account.Queries.CheckRefreshToken
{
    public class RefreshTokenQuery : IRequest<AddRefreshTokenCommand>
    {
        public Guid IdPerson { get; set; }
        public string RefreshToken { get; set; }

        public class Handler : IRequestHandler<RefreshTokenQuery, AddRefreshTokenCommand>
        {
            private readonly GakkoBackendContext _context;
            private readonly IMediator _mediator;

            public Handler(GakkoBackendContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<AddRefreshTokenCommand> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var employeeFromDb =
                    await _context.Employee
                      .SingleOrDefaultAsync(x => x.IdEmployee == request.IdPerson && x.RefreshToken == request.RefreshToken, cancellationToken);
                var personFromDb =
                    await _context.Person
                        .SingleOrDefaultAsync(x => x.IdPerson == request.IdPerson);

                return employeeFromDb == null ? null : new AddRefreshTokenCommand { Person = personFromDb, Employee = employeeFromDb };
            }
        }
    }
}
