using System;

namespace ApplicationServices
{
    public class AggreggateEvent
    {
        public Guid Id { get; private set; }

        private DateTime EventTime { get; set; }

        public AggreggateEvent()
        {
            this.Id = Guid.NewGuid();
            this.EventTime = DateTime.Now;
        }
    }
}