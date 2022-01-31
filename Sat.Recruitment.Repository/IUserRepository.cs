using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Models.Models;

namespace Sat.Recruitment.Repository
{
    public interface IUserRepository
    {
        Task<IList<UserModel>> GetAllUsers();
    }
}
