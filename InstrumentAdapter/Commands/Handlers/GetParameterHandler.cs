using ApplicationServices;
using InstrumentCommunication.TsnAdapter;
using InstrumentCommunication.TsnAdapter.Commands;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class GetParameterHandler:IHandler<GetParameterCommand, IResponse>
    {
        private readonly ITSNAdapter tsnAdapter;

        public GetParameterHandler(ITSNAdapter tsnAdapter)
        {
            this.tsnAdapter = tsnAdapter;
        }

        public IResponse Handle(GetParameterCommand command)
        {
            tsnAdapter.Handle(new DownloadParameterCommand(command.FileName));

            return new CommandSuccesfullyHandled();
        }
    }
}