namespace Infrastructure.EventBroker
{
    public interface IEventHandler
    {
        void Handle(IEvent @event);
    }
}