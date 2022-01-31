using Sat.Recruitment.Infra;

namespace Sat.Recruitment.Services.Implementation.Providers
{
    public class CalculateMoneyPremiumUserProvider : ICalculateMoneyProvider
    {
        public UserType UserType => UserType.Premium;

        public decimal CalculateMoney(decimal money)
        {
            decimal moneyToResult = money;

            if (money > 100)
            {
                decimal gif = money * 2;
                moneyToResult = money + gif;
            }

            return moneyToResult;
        }
    }
}
