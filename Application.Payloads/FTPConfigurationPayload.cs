using LabConfiguration.Domain;

namespace Application.Payloads
{
    public class FTPConfigurationPayload:IPayloadObject
    {
        private readonly FTPConfiguration ftpConfiguration;

        public FTPConfigurationPayload(FTPConfiguration configuration)
        {
            this.ftpConfiguration = configuration;
        }

        public string DataBasket { get { return ftpConfiguration.DataBasket; } }
        public string Password { get { return ftpConfiguration.Password; } }
        public string User { get { return ftpConfiguration.User; } }
    }
}