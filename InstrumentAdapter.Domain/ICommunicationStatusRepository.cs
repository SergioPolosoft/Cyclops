namespace InstrumentAdapter.Domain
{
    public interface ICommunicationStatusRepository
    {
        void WaitForMessages();
        CommunicationStatus GetCurrentStatus();
        void WaitForAcknowledge();
    }
}