using System;
using System.Collections.Generic;
using System.Linq;
using Sat.Recruitment.Infra;

namespace Sat.Recruitment.Services.Implementation.Providers
{
    public class CalculateMoneyNormalUserProvider : ICalculateMoneyProvider
    {
        public UserType UserType => UserType.Normal;
        private readonly Dictionary<int, Func<decimal, decimal>> logicFuntions;

        public CalculateMoneyNormalUserProvider()
        {
            logicFuntions = new Dictionary<int, Func<decimal, decimal>>
                            {
                                {1, GetGifMoneyGreaterThan100},
                                {2, GetGifMoneyGreaterThan10}
                            };
        }


        public decimal CalculateMoney(decimal money)
        {
            decimal gif = 0;

            foreach (KeyValuePair<int, Func<decimal, decimal>> function in logicFuntions.OrderBy(c => c.Key).ToList())
            {
                gif = function.Value.Invoke(money);
                if (gif > 0)
                    break;
            }

            return money + gif;
        }

        private static decimal GetGifMoneyGreaterThan100(decimal money)
        {
            decimal gif = 0;

            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                gif = money * percentage;
            }

            return gif;
        }

        private static decimal GetGifMoneyGreaterThan10(decimal money)
        {
            decimal gif = 0;

            if (money > 10)
            {
                var percentage = Convert.ToDecimal(0.8);
                gif = money * percentage;
            }
            return gif;
        }
    }
}



