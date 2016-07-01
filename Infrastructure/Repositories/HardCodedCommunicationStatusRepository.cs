using InstrumentAdapter.Domain;

namespace Infrastructure.Repositories
{
    public class HardCodedCommunicationStatusRepository : ICommunicationStatusRepository
    {
        private CommunicationStatus status;

        public void WaitForMessages()
        {
            this.status = CommunicationStatus.WaitingForMessages;
        }

        public CommunicationStatus GetCurrentStatus()
        {
            return status;
        }

        public void WaitForAcknowledge()
        {
            this.status = CommunicationStatus.WaitingForAcknowledge;
        }
    }
}