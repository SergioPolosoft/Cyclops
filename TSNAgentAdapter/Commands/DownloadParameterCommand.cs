using ApplicationServices;

namespace InstrumentCommunication.TsnAdapter.Commands
{
    public class DownloadParameterCommand : ICommand
    {
        private string fileName;

        public DownloadParameterCommand(string fileName)
        {
            this.fileName = fileName;
        }
    }
}