using InstrumentCommunication.Application.Commands;
using InstrumentCommunication.Application.Ports;
using InstrumentCommunication.Application.Responses;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;

namespace LabConfigurationAdapter
{
    public class LabConfigurationAdapter : ILabConfigurationPort
    {
        private readonly ILabConfigurationServices labConfigurationServices;
        
        public LabConfigurationAdapter(ILabConfigurationServices labConfigurationServices)
        {
            this.labConfigurationServices = labConfigurationServices;
        }

        public void Handle(ParameterConfirmationCommand parameterConfirmationCommand)
        {
            var labConfigurationServicesCommand = new CommandTranslator(parameterConfirmationCommand).Translate();
            labConfigurationServices.Handle(labConfigurationServicesCommand);
        }

        public FTPConfigurationDTO GetFTPConfiguration()
        {
            var response = labConfigurationServices.Handle(new GetFTPConfiguration());
            var configuration = ((GetFTPConfigurationResponse) response).FTPConfiguration;
            return new FTPConfigurationDTO()
            {
                DataBasket = configuration.DataBasket,
                Password = configuration.Password,
                User = configuration.User
            };
        }

        public void SaveMainUnitFTPConfiguration(FTPConfigurationDTO mainUnitConfiguration)
        {
            labConfigurationServices.Handle(new StoreMainunitConfigurationCommand(mainUnitConfiguration.User, mainUnitConfiguration.DataBasket, mainUnitConfiguration.Password));
        }
    }
}