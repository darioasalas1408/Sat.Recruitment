using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Models.IO;
using Sat.Recruitment.Services;
using SharpTestsEx;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recruitment.Api
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTest
    {
        [Fact]
        public async Task Should_Return_Success_When_User_is_Created()
        {
            var validator = new Mock<IUserValidator>();
            var userService = new Mock<IUserService>();

            var user = new UserInput()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                UserType = "Normal",
                Money = 124,
                Address = "Av. Juan G",
                Phone = "+349 1122354215"
            };

            userService.Setup(s => s.CreateUser(user)).ReturnsAsync(true);
            validator.Setup(s => s.Validate(user)).Returns(new FluentValidation.Results.ValidationResult());

            var userController = new UsersController(validator.Object, userService.Object);

            OkObjectResult result = (OkObjectResult)await userController.CreateUser(user);

            result.StatusCode.Should().Be.EqualTo(200);
            result.Value.Should().Be.Equals("User Created");
        }

        [Fact]
        public async Task Should_Return_Fail_When_User_is_Duplicated()
        {
            var validator = new Mock<IUserValidator>();
            var userService = new Mock<IUserService>();

            var user = new UserInput()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                UserType = "Normal",
                Money = 124,
                Address = "Av. Juan G",
                Phone = "+349 1122354215"
            };

            userService.Setup(s => s.CreateUser(user)).ReturnsAsync(false);
            validator.Setup(s => s.Validate(user)).Returns(new FluentValidation.Results.ValidationResult());

            var userController = new UsersController(validator.Object, userService.Object);

            BadRequestObjectResult result = (BadRequestObjectResult)await userController.CreateUser(user);

            result.StatusCode.Should().Be.EqualTo(400);
            result.Value.Should().Be.Equals("The user is duplicated");
        }
    }
}
