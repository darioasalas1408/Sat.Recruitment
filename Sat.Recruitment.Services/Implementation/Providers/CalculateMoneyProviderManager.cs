using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Sat.Recruitment.Infra;

namespace Sat.Recruitment.Services.Implementation.Providers
{
    public class CalculateMoneyProviderManager : ICalculateMoneyProviderManager
    {
        private readonly IReadOnlyDictionary<UserType, ICalculateMoneyProvider> calculateMoneyProviders;

        public CalculateMoneyProviderManager()
        {
            const decimal money = 0;

            Type calculateMoneyProviderType = typeof(ICalculateMoneyProvider);

            calculateMoneyProviders = calculateMoneyProviderType.Assembly.ExportedTypes
                .Where(x => calculateMoneyProviderType.IsAssignableFrom(x) && !x.IsInterface)
                .Select(x =>
                 {
                     ConstructorInfo parameterlessCtor = x.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
                     return parameterlessCtor != null ? Activator.CreateInstance(x) : Activator.CreateInstance(x, money);

                 })
                .Cast<ICalculateMoneyProvider>()
                .ToImmutableDictionary(x => x.UserType);

        }

        public ICalculateMoneyProvider GetICalculateMoneyProvider(UserType userType)
        {
            ICalculateMoneyProvider provider = calculateMoneyProviders.GetValueOrDefault(userType);
            return provider ?? calculateMoneyProviders[UserType.Normal];
        }
    }
}
