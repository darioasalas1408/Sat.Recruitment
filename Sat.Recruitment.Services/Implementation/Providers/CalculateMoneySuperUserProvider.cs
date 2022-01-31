using System;
using Sat.Recruitment.Infra;

namespace Sat.Recruitment.Services.Implementation.Providers
{
    public class CalculateMoneySuperUserProvider : ICalculateMoneyProvider
    {
        public UserType UserType => UserType.SuperUser;

        public decimal CalculateMoney(decimal money)
        {
            decimal moneyToResult = money;

            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                decimal gif = money * percentage;
                moneyToResult = money + gif;
            }

            return moneyToResult;
        }
    }
}
