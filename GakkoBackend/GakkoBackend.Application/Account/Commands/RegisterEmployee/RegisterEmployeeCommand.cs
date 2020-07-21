using GakkoBackend.Domain;
using GakkoBackend.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GakkoBackend.Application.Account.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommand: IRequest<bool>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<RegisterEmployeeCommand, bool>
        {
            private readonly GakkoBackendContext _context;
            private readonly IMediator _mediator;

            public Handler(GakkoBackendContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<bool> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
            {
                var personFromDb = await _context.Person.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

                if (personFromDb != null) return false;

                var person = new Person
                {
                    IdPerson = Guid.NewGuid(),
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    Phone = request.Phone,
                    Image = request.Image ?? null,
                    Gender = request.Gender
                };

                await _context.Person.AddAsync(person, cancellationToken);

                var employee = new Employee
                {
                    IdEmployee = person.IdPerson,

                };

                employee.PasswordHash = new PasswordHasher<Employee>().HashPassword(employee, request.Password);

                await _context.Employee.AddAsync(employee, cancellationToken);

                return await _context.SaveChangesAsync(cancellationToken) > 0;

            }
        }
    }
}
