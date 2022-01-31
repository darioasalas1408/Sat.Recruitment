using System.Collections.Generic;
using Sat.Recruitment.Repository;
using System.Linq;
using Sat.Recruitment.Models;
using System.Threading.Tasks;
using Sat.Recruitment.Models.IO;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Models.Models.Mapping;
using Sat.Recruitment.Infra;

namespace Sat.Recruitment.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ICalculateMoneyProviderManager calculateMoneyProviderManager;
        

        public UserService(IUserRepository userRepository, ICalculateMoneyProviderManager calculateMoneyProviderManager)
        {
            this.userRepository = userRepository;
            this.calculateMoneyProviderManager = calculateMoneyProviderManager;
            
        }

        public async Task<bool> IsDuplicated(UserModel newUser)
        {
            IList<UserModel> allUsers = await userRepository.GetAllUsers();

            return allUsers.Any(w => w.Email == newUser.Email || w.Phone == newUser.Phone);
        }

        public async Task<bool> CreateUser(UserInput userInputData)
        {
            UserModel newUser = userInputData.ToModel();

            bool isUserDuplicated = await IsDuplicated(newUser);
            if (isUserDuplicated)
            {
                return false;
            }

            ICalculateMoneyProvider providerToUse = calculateMoneyProviderManager.GetICalculateMoneyProvider(newUser.UserType);
            newUser.Money = providerToUse.CalculateMoney(newUser.Money);

            return true;
        }
    }
}
