using ApplicationServices;
using InstrumentCommunication.TsnAdapter;
using InstrumentCommunication.TsnAdapter.Commands;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class GetParameterListHandler:IHandler<ICommand, IResponse>
    {
        private readonly ITSNAdapter tsnAdapter;

        public GetParameterListHandler(ITSNAdapter tsnAdapter)
        {
            this.tsnAdapter = tsnAdapter;
        }

        public IResponse Handle(ICommand command)
        {
            tsnAdapter.Handle(new DownloadListOfParameters());
            return new CommandSuccesfullyHandled();
        }
    }
}