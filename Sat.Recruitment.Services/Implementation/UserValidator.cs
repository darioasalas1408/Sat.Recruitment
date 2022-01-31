using FluentValidation;
using Sat.Recruitment.Models.IO;

namespace Sat.Recruitment.Services.Implementation
{
    public class UserValidator : AbstractValidator<UserInput>, IUserValidator
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The email is required").EmailAddress().WithMessage("The email is not valid");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The address is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("The phone is required");
        }
    }
}
