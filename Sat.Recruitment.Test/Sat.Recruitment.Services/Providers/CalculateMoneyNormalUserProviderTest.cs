using Sat.Recruitment.Services.Implementation.Providers;
using SharpTestsEx;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recruitment.Services.Providers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CalculateMoneyNormalUserProviderTest
    {
        [Fact]
        public void Should_Calculate_Correct_Money_GreaterThan100()
        {
            var initialMoney = 125;
            var provider = new CalculateMoneyNormalUserProvider();

            var totalMoney = provider.CalculateMoney(initialMoney);
            totalMoney.Should().Be.EqualTo(initialMoney*1.12);
        }

        [Fact]
        public void Should_Calculate_Correct_Money_GreaterThan10()
        {
            var initialMoney = 15;
            var provider = new CalculateMoneyNormalUserProvider();

            var finalResult = initialMoney * 1.8;
            var totalMoney = provider.CalculateMoney(initialMoney);
            totalMoney.Should().Be.EqualTo(finalResult);
        }

        [Fact]
        public void Should_Calculate_Correct_Money_GreaterThan0()
        {
            var initialMoney = 5;
            var provider = new CalculateMoneyNormalUserProvider();

            var totalMoney = provider.CalculateMoney(initialMoney);
            totalMoney.Should().Be.EqualTo(initialMoney);
        }
    }
}
