using System;

namespace Sat.Recruitment.Infra
{
    public static class EnumExtensions
    {
        public static T ParseEnum<T>(string value)
        {
            try
            {
                var result = (T)Enum.Parse(typeof(T), value, true);
                return result;
            }
            catch (Exception)
            {
                return (T)Enum.Parse(typeof(T), "Normal", true);
            }
        }
    }
}
