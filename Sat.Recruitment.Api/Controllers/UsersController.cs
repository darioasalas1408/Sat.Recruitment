using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Services;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Sat.Recruitment.Models.IO;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserValidator userValidator;
        private readonly IUserService userService;

        public UsersController(IUserValidator userValidator, IUserService userService)
        {
            this.userValidator = userValidator;
            this.userService = userService;
        }

        //TODO: In this scenario, object ResultApi was removed because we are not adding any special data in there. No specific error codes
        [HttpPost]
        [Route("/create-userInput")]
        public async Task<ActionResult> CreateUser(UserInput userInput)
        {
            try
            {
                ValidationResult validationResult = userValidator.Validate(userInput);
                if (!validationResult.IsValid)
                {
                    //return new ResultApi(false, validationResult.Errors.First().ErrorMessage);
                    return BadRequest(validationResult.Errors.First().ErrorMessage);
                }

                bool userCreated = await userService.CreateUser(userInput);

                if (userCreated)
                {
                    return Ok("User Created");
                }
                else
                {
                    return BadRequest("The user is duplicated");
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
