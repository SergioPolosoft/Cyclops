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
            this.Installed = true;
        }

        public void When(QualityControlUpdated qualityControlUpdated)
        {
            this.StandardDeviation = qualityControlUpdated.StandardDeviation;
            this.TargetValue = qualityControlUpdated.TargetValue;
        }

        public double StandardDeviation { get; private set; }

        public double TargetValue { get; private set; }

        public int TestCode { get; private set; }

        public Guid ControlId { get; private set; }

        public bool Installed{get; private set;}
    }
}