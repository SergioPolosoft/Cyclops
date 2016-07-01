using System.Collections.Generic;

namespace ApplicationServices
{
    public abstract class Aggreggate<T> where T:State, new()
    {
        protected T state;
        public IList<AggreggateEvent> Changes { get; private set; }

        protected Aggreggate(IEnumerable<AggreggateEvent> aggreggateEvents)
        {
            Changes = new List<AggreggateEvent>();
            this.state = new T();
            foreach (var aggreggateEvent in aggreggateEvents)
            {
                this.state.Mutate(aggreggateEvent);
            }
        }

        protected void Apply(AggreggateEvent aggreggateEvent)
        {
            this.state.Mutate(aggreggateEvent);

            this.Changes.Add(aggreggateEvent);
        }
    }
}