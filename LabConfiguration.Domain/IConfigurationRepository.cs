namespace LabConfiguration.Domain
{
    public interface IConfigurationRepository
    {
        FTPConfiguration GetFTPConfiguration();
        void SaveMainUnitFTPConfiguration(FTPConfiguration ftpConfiguration);
    }
}