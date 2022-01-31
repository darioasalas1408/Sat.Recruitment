using Microsoft.Extensions.Configuration;

namespace Sat.Recruitment.Infra
{
    public class GeneralSetting : IGeneralSetting
    {
        private readonly IConfiguration configuration;

        public GeneralSetting(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string UrlFile => this.configuration["GeneralSetting:URLFile"];
    }
}
