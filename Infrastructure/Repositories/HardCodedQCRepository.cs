using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Events;
using QCConfiguration.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class HardCodedQCRepository : IQualityControlRepository
    {
        private readonly IList<AggreggateEvent> qcevents = new List<AggreggateEvent>();

        public QualityControl GetQualityControlForTestCode(int testCode)
        {
            var creationEvent = qcevents.OfType<QualityControlCreated>().FirstOrDefault(x => x.TestCode == testCode);
            if (creationEvent==null)
            {
                return null;
            }
            var events = qcevents.OfType<QualityControlEvent>().Where(x => x.ControlId == creationEvent.ControlId);
            return new QualityControl(events);
        }

        public void Add(IList<AggreggateEvent> events)
        {
            foreach (var @event in events)
            {
                this.qcevents.Add(@event);
            }
        }
    }
}