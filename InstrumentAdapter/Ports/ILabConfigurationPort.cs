using InstrumentCommunication.Application.Commands;

namespace InstrumentCommunication.Application.Ports
{
    public interface ILabConfigurationPort
    {
        void Handle(ParameterConfirmationCommand parameterConfirmationCommand);
        FTPConfigurationDTO GetFTPConfiguration();
        void SaveMainUnitFTPConfiguration(FTPConfigurationDTO mainUnitConfiguration);
    }
}