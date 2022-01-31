using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Infra;
using Sat.Recruitment.Models.Models;

namespace Sat.Recruitment.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly string urlFile;

        public UserRepository(string urlFile)
        {
            this.urlFile = urlFile;
        }

        //TODO: Consider using some package to better read of the file. In real world scenario, this would come from a database
        //TODO: This method should return a UserDao
        public async Task<IList<UserModel>> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            string path = Path.Combine(urlFile, "Users.txt");

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var lineItems = line.Split(",");

                var user = new UserModel
                {
                    Name = GetElementIfExist(lineItems, 0),
                    Email = GetElementIfExist(lineItems, 1),
                    Phone = GetElementIfExist(lineItems, 2),
                    Address = GetElementIfExist(lineItems, 3),
                    UserType = EnumExtensions.ParseEnum<UserType>(GetElementIfExist(lineItems, 4))
                };

                decimal.TryParse(GetElementIfExist(lineItems, 5), out var money);
                user.Money = money; 

                users.Add(user);
            }

            reader.Close();

            return users;
        }

        private string GetElementIfExist(string[] lineItems, int index)
        {
            if (lineItems.Length >= index)
            {
                return lineItems[index]; 
            }

            return string.Empty; 
        }
    }
}
