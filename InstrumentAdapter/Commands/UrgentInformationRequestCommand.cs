using InstrumentAdapter.Domain;

namespace InstrumentCommunication.Application.Commands
{
    public class UrgentInformationRequestCommand:ICommandWithMessage
    {
        private readonly UrgentInformationMessage message;

        public UrgentInformationRequestCommand(UrgentInformationMessage urgentInformationMessage)
        {
            this.message = urgentInformationMessage;
        }

        public Message Message {
            get { return message; }
        }
    }
}