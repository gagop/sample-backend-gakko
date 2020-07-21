using FluentValidation;
using GakkoBackend.Application.Account.Queries.LoginEmployee;

namespace GakkoBackend.Application.Account.Queries.LoginPerson
{
    public class LoginPersonQueryValidation: AbstractValidator<LoginEmployeeQuery>
    {
        //public LoginPersonQueryValidation()
        //{
        //    RuleFor(x => x.Email).NotEmpty();
        //    RuleFor(x => x.Password).NotEmpty();
        //}
    }
}
