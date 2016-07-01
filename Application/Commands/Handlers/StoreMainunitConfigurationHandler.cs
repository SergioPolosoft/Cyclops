using ApplicationServices;
using LabConfiguration.Domain;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class StoreMainunitConfigurationHandler : IHandler<StoreMainunitConfigurationCommand,IResponse>
    {
        private readonly IConfigurationRepository configurationRepository;

        public StoreMainunitConfigurationHandler(IConfigurationRepository configurationRepository)
        {
            this.configurationRepository = configurationRepository;
        }

        public IResponse Handle(StoreMainunitConfigurationCommand command)
        {
            this.configurationRepository.SaveMainUnitFTPConfiguration(command.FTPConfiguration);
            return new CommandSuccesfullyHandled();
        }
    }
}