using Sat.Recruitment.Services.Implementation.Providers;
using SharpTestsEx;
using System;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recruitment.Services.Providers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CalculateMoneySuperUserProviderTest
    {
        [Fact]
        public void Should_Calculate_Correct_Money_GreaterThan100()
        {
            var initialMoney = TestHelper.GetRandomInt(100, 200);
            var provider = new CalculateMoneySuperUserProvider();

            var totalMoney = provider.CalculateMoney(initialMoney);
            totalMoney.Should().Be.EqualTo(Math.Round(initialMoney * 1.2, 2));
        }
    }
}
