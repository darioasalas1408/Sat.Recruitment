using FluentValidation.Results;
using Sat.Recruitment.Models.IO;

namespace Sat.Recruitment.Services
{
    public interface IUserValidator
    {
        ValidationResult Validate(UserInput newUserInput);
    }
}
