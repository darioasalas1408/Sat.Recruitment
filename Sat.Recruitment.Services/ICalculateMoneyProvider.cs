using Sat.Recruitment.Infra;

namespace Sat.Recruitment.Services
{
    public interface ICalculateMoneyProvider
    {
        decimal CalculateMoney(decimal money);

        UserType UserType { get; }
    }
}
