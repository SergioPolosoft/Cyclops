using ApplicationServices;
using InstrumentCommunication.Application.Ports;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class StoreMainUnitFtpCredentialsHandler:IHandler<StoreMainUnitFTPCredentialsCommand,IResponse>
    {
        private readonly ILabConfigurationPort labConfigurationServices;

        public StoreMainUnitFtpCredentialsHandler(ILabConfigurationPort labConfigurationServices)
        {
            this.labConfigurationServices = labConfigurationServices;
        }

        public IResponse Handle(StoreMainUnitFTPCredentialsCommand command)
        {
            var mainUnitConfiguration = new FTPConfigurationDTO
            {
                User = command.User,
                DataBasket = command.DataBasket
            };

            labConfigurationServices.SaveMainUnitFTPConfiguration(mainUnitConfiguration);
            return new CommandSuccesfullyHandled();
        }
    }
}