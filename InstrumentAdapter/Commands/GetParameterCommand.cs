using ApplicationServices;

namespace InstrumentCommunication.Application.Commands
{
    public class GetParameterCommand : ICommand
    {
        private readonly string fileName;

        public GetParameterCommand(string fileName)
        {
            this.fileName = fileName;
        }

        public string FileName
        {
            get { return fileName; }
        }
    }
}