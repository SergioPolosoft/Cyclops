using System;
using System.Collections.Generic;
using ApplicationServices;
using QCConfiguration.Domain.Events;

namespace QCConfiguration.Domain.States
{
    public class QualityControlState : State
    {
        public QualityControlState(IEnumerable<AggreggateEvent> changesForTheId) : base(changesForTheId)
        {
        }

        public QualityControlState() : base(new List<AggreggateEvent>())
        {
            
        }

        public void When(QualityControlCreated qualityControlCreated)
        {
            this.ControlId = qualityControlCreated.ControlId;
        }

        public Guid ControlId { get; set; }
    }
}