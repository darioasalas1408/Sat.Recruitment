using System;

namespace Sat.Recruitment.Test.Sat.Recruitment.Services.Providers
{
    public static class TestHelper
    {
        public static int GetRandomInt(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
