using Sat.Recruitment.Infra;

namespace Sat.Recruitment.Services
{
    public interface ICalculateMoneyProviderManager
    {
        ICalculateMoneyProvider GetICalculateMoneyProvider(UserType userType);
    }
}
