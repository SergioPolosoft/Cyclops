using System;
using System.Collections.Generic;

namespace Infrastructure.EventBroker
{
    public class CodedEventBroker : IEventBroker
    {
        private readonly IList<IEvent> events = new List<IEvent>();
        private readonly IDictionary<Type,List<IEventHandler>> subscribers = new Dictionary<Type, List<IEventHandler>>();

        public void RaiseEvent(IEvent @event)
        {
            this.events.Add(@event);
            if (subscribers.ContainsKey(@event.GetType()))
            {
                foreach (var handler in subscribers[@event.GetType()])
                {
                    handler.Handle(@event);
                }
            }
        }

        public void Subscribe<T>(IEventHandler eventHandler) where T:IEvent
        {
            if (subscribers.ContainsKey(typeof(T)))
            {
                subscribers[typeof (T)].Add(eventHandler);
            }
            else
            {
                subscribers.Add(typeof(T), new List<IEventHandler>(){eventHandler});
            }
        }
    }
}