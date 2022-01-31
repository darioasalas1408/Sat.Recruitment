using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Sat.Recruitment.Models.IO;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Repository;
using Sat.Recruitment.Services;
using Sat.Recruitment.Services.Implementation;
using SharpTestsEx;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recruitment.Services
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserServiceTest
    {
        [Fact]
        public async Task Should_Detect_Duplicate_Record_Not_Call_Calculate_Money()
        {

            var userList = FixtureSingleton.Instance.CreateMany<UserModel>();
            userList.First().Email = "mike@gmail.com";

            BuildUserService(out Mock<IUserRepository> userRepositoryMock,
                out Mock<ICalculateMoneyProviderManager> calculateMoneyFactory,
                out Mock<ICalculateMoneyProvider> normalUserProvider,
                out UserService userService);

            userRepositoryMock.Setup(s => s.GetAllUsers()).ReturnsAsync(userList.ToList());
            calculateMoneyFactory.Setup(s => s.GetICalculateMoneyProvider(Infra.UserType.Normal)).Returns(normalUserProvider.Object);
            normalUserProvider.Setup(s => s.CalculateMoney(It.IsAny<decimal>())).Returns(100);

            var userToCheck = FixtureSingleton.Instance.Create<UserModel>();
            userToCheck.Email = "mike+15@gmail.com";

            var result = await userService.IsDuplicated(userToCheck);

            result.Should().Be(false);
            userRepositoryMock.Verify(s => s.GetAllUsers(), Times.Once);
            calculateMoneyFactory.Verify(s => s.GetICalculateMoneyProvider(Infra.UserType.Normal), Times.Never);
            normalUserProvider.Verify(s => s.CalculateMoney(It.IsAny<decimal>()), Times.Never);
        }

        [Fact]
        public async Task Should_Return_True_When_User_is_Created()
        {
            BuildUserService(out Mock<IUserRepository> userRepositoryMock,
                out Mock<ICalculateMoneyProviderManager> calculateMoneyFactory,
                out Mock<ICalculateMoneyProvider> normalUserProvider,
                out UserService userService);

            userRepositoryMock.Setup(s => s.GetAllUsers()).ReturnsAsync(new List<UserModel>());
            calculateMoneyFactory.Setup(s => s.GetICalculateMoneyProvider(Infra.UserType.Normal)).Returns(normalUserProvider.Object);
            normalUserProvider.Setup(s => s.CalculateMoney(It.IsAny<decimal>())).Returns(100);

            var user = FixtureSingleton.Instance.Create<UserInput>();

            var result = await userService.CreateUser(user);
            result.Should().Be(true);
        }

        [Fact]
        public async Task Should_Return_False_When_User_is_Duplicated()
        {
            var userList = FixtureSingleton.Instance.CreateMany<UserModel>();
            userList.First().Email = "mike@gmail.com";

            BuildUserService(out Mock<IUserRepository> userRepositoryMock,
                out Mock<ICalculateMoneyProviderManager> calculateMoneyFactory,
                out Mock<ICalculateMoneyProvider> normalUserProvider,
                out UserService userService);

            userRepositoryMock.Setup(s => s.GetAllUsers()).ReturnsAsync(userList.ToList());
            calculateMoneyFactory.Setup(s => s.GetICalculateMoneyProvider(Infra.UserType.Normal)).Returns(normalUserProvider.Object);
            normalUserProvider.Setup(s => s.CalculateMoney(It.IsAny<decimal>())).Returns(100);


            var user = FixtureSingleton.Instance.Create<UserInput>();
            user.Email = "mike+15@gmail.com";


            var result = await userService.CreateUser(user);
            result.Should().Be(false);
        }

        private static void BuildUserService(out Mock<IUserRepository> userRepositoryMock, out Mock<ICalculateMoneyProviderManager> calculateMoneyFactory, out Mock<ICalculateMoneyProvider> normalUserProvider, out UserService userService)
        {
            userRepositoryMock = new Mock<IUserRepository>();
            calculateMoneyFactory = new Mock<ICalculateMoneyProviderManager>();
            normalUserProvider = new Mock<ICalculateMoneyProvider>();
            userService = new UserService(userRepositoryMock.Object, calculateMoneyFactory.Object);
        }
    }
}
