using System;
using System.Collections.Generic;
using ApplicationServices;
using QCConfiguration.Domain.Events;

namespace QCConfiguration.Domain
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
            this.TestCode = qualityControlCreated.TestCode;
            this.TargetValue = qualityControlCreated.TargetValue;
            this.StandardDeviation = qualityControlCreated.StandardDeviation;
        }

        public double StandardDeviation { get; set; }

        public double TargetValue { get; set; }

        public int TestCode { get; set; }

        public Guid ControlId { get; set; }
    }
}