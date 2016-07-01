namespace Infrastructure.EventBroker
{
    public interface IEventBroker
    {
        void RaiseEvent(IEvent @event);
        void Subscribe<T>(IEventHandler eventHandler) where T:IEvent;
    }
}