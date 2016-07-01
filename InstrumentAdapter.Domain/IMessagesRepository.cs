namespace InstrumentAdapter.Domain
{
    public interface IMessagesRepository
    {
        void Save(Message message);
    }
}