using ApplicationServices;
using LabConfiguration.Domain;

namespace LabConfiguration.Application.Commands
{
    public class StoreMainunitConfigurationCommand : ICommand
    {
        public StoreMainunitConfigurationCommand(string user, string dataBasket, string password)
        {
            this.FTPConfiguration = new FTPConfiguration
            {
                User = user,
                DataBasket = dataBasket,
                Password = password
            };
        }
        public FTPConfiguration FTPConfiguration { get; private set; }
    }
}