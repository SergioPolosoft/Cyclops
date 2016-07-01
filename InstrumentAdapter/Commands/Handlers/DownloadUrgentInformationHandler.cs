using ApplicationServices;
using InstrumentCommunication.TsnAdapter;
using InstrumentCommunication.TsnAdapter.Commands;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class DownloadUrgentInformationHandler:IHandler<ICommand,IResponse>
    {
        private readonly ITSNAdapter tsnAdapter;

        public DownloadUrgentInformationHandler(ITSNAdapter tsnAdapter)
        {
            this.tsnAdapter = tsnAdapter;
        }

        public IResponse Handle(ICommand command)
        {
            tsnAdapter.Handle(new DownloadUrgentInformation());

            return new CommandSuccesfullyHandled();
        }        
    }
}