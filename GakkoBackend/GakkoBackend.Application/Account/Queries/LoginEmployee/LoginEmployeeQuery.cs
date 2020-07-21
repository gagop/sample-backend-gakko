using GakkoBackend.Application.Account.Commands.AddRefreshToken;
using GakkoBackend.Application.Exceptions;
using GakkoBackend.Domain;
using GakkoBackend.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GakkoBackend.Application.Account.Queries.LoginEmployee
{
    public class LoginEmployeeQuery : IRequest<AddRefreshTokenCommand>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginEmployeeQuery, AddRefreshTokenCommand>
        {
            private readonly GakkoBackendContext _context;
            private readonly IMediator _mediator;

            public Handler(GakkoBackendContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<AddRefreshTokenCommand> Handle(LoginEmployeeQuery request, CancellationToken cancellationToken)
            {
                var personFromDb = await _context.Person
                    .SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

                if (personFromDb == null) return null;

                var employeeFromDb = await _context.Employee
                    .SingleOrDefaultAsync(x => x.IdEmployee == personFromDb.IdPerson, cancellationToken);

                if (new PasswordHasher<Employee>().VerifyHashedPassword(employeeFromDb, employeeFromDb.PasswordHash, request.Password) ==
                    PasswordVerificationResult.Failed)
                {
                    throw new AuthorizeException("Incorrect login or password");
                }


                return new AddRefreshTokenCommand { Employee = employeeFromDb, Person = personFromDb };
            }
        }
    }
}
