using EmpMgmt.Core.DTOs;
using FluentValidation;

namespace EmpMgmt.Core.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(e => e.Position)
                .NotEmpty().WithMessage("Position is required.");
        }
    }
}
