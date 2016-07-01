using System;
using ApplicationServices;

namespace QCConfiguration.Domain.Events
{
    public abstract class QualityControlEvent:AggreggateEvent
    {
        protected QualityControlEvent(Guid controlId)
        {
            this.ControlId = controlId;
        }

        public Guid ControlId { get; set; }
    }
}