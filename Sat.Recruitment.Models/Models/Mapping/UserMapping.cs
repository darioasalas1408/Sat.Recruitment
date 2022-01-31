using Sat.Recruitment.Infra;
using Sat.Recruitment.Models.IO;

namespace Sat.Recruitment.Models.Models.Mapping
{
    //TODO: We can implement AutoMapper here
    public static class UserMapping
    {
        public static UserModel ToModel(this UserInput userInput)
        {
            if (userInput == null)
            {
                return null;
            }

            var userModel = new UserModel()
            {
                Address = userInput.Address,
                Email = GetCleanEmailForm(userInput.Email),
                Money = userInput.Money,
                Name = userInput.Name,
                Phone = userInput.Phone,
                UserType = EnumExtensions.ParseEnum<UserType>(userInput.UserType)
            };

            return userModel;
        }

        //TODO: Improve this code
        private static string GetCleanEmailForm(string email)
        {
            string[] array = email.Split("+");
            if (array.Length == 1)
            {
                return email;
            }

            string firstPart = array[0];
            string secondPart = array[1];

            array = secondPart.Split("@");
            if (array.Length == 1)
            {
                return email;
            }

            return $"{firstPart}@{array[1]}";
        }
    }
}


