using ApplicationServices;
using InstrumentCommunication.Application.Ports;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class ParameterConfirmationHandler:IHandler<ParameterConfirmationCommand,IResponse>
    {
        private readonly ILabConfigurationPort labConfigurationPort;

        public ParameterConfirmationHandler(ILabConfigurationPort labConfigurationPort)
        {
            this.labConfigurationPort = labConfigurationPort;
        }

        public IResponse Handle(ParameterConfirmationCommand command)
        {
            labConfigurationPort.Handle(command);
            return new CommandSuccesfullyHandled();
        }
    }
}