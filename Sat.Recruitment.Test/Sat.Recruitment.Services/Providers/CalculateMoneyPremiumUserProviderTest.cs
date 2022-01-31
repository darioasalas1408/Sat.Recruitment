using Sat.Recruitment.Services.Implementation.Providers;
using SharpTestsEx;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recruitment.Services.Providers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CalculateMoneyPremiumUserProviderTest
    {
        [Fact]
        public void Should_Calculate_Correct_Money_GreaterThan100()
        {
            var initialMoney = TestHelper.GetRandomInt(10, 100);
            var provider = new CalculateMoneyPremiumUserProvider();

            var totalMoney = initialMoney +  provider.CalculateMoney(initialMoney);
            totalMoney.Should().Be.EqualTo(totalMoney);
        }
    }
}
