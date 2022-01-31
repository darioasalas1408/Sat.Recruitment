using AutoFixture;

namespace Sat.Recruitment.Test.Sat.Recruitment.Services
{
    public sealed class FixtureSingleton : Fixture
    {
        private static FixtureSingleton instance;

        private FixtureSingleton()
        {

        }

        public static FixtureSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FixtureSingleton();
                }

                return instance;
            }
        }
    }
}
