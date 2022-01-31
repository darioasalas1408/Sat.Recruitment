using System.Threading.Tasks;
using Sat.Recruitment.Models.IO;
using Sat.Recruitment.Models.Models;

namespace Sat.Recruitment.Services
{
    public interface IUserService
    {
        Task<bool> IsDuplicated(UserModel newUser);

        Task<bool> CreateUser(UserInput newUserInput);
    }
}
