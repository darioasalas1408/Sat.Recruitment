using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Repository.Implementation;
using SharpTestsEx;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recruitment.Repository
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserRepositoryTest
    {
        [Fact]
        public async Task Should_Read_Users_From_Txt()
        {
            var repository = new UserRepository(@"C:\\Repos\\Ejercicio\\Sat.Recruitment\\Sat.Recruitment\\Sat.Recruitment.Api/Files/");
            var users = await repository.GetAllUsers();
            users.Count().Should().Be.GreaterThan(0);
        }
    }
}
