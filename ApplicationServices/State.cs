using System.Collections.Generic;

namespace ApplicationServices
{
    public class State
    {
        public State(IEnumerable<AggreggateEvent> changesForTheId)
        {
            foreach (var domainEvent in changesForTheId)
            {
                this.Mutate(domainEvent);
            }
        }

        public void Mutate(AggreggateEvent aggreggateEvent)
        {
            ((dynamic)this).When((dynamic)aggreggateEvent);
        }
    }
}