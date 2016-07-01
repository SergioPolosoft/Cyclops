using System;
using LabConfiguration.Domain;

namespace Infrastructure.Repositories
{
    public class HardCodedConfigurationRepository : IConfigurationRepository
    {
        public FTPConfiguration GetFTPConfiguration()
        {
            return new FTPConfiguration()
            {
                User = "DMFTPUser",
                DataBasket = "DMFTPBasket"
            };
        }

        public void SaveMainUnitFTPConfiguration(FTPConfiguration ftpConfiguration)
        {
            this.MainUitFTPConfiguration = ftpConfiguration;
        }

        public void Add(Application application)
        {
            throw new System.NotImplementedException();
        }

        public bool ApplicationExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public FTPConfiguration MainUitFTPConfiguration { get; set; }
    }
}