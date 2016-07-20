using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using QCConfiguration.Domain.Events;

namespace QCConfiguration.Domain
{
    public class QualityControl:Aggreggate<QualityControlState>
    {
        public QualityControl(IEnumerable<QualityControlEvent> aggreggateEvents) : base(aggreggateEvents)
        {
        }

        public QualityControl(params AggreggateEvent[] aggreggateEvents):base(aggreggateEvents.ToList())
        {
        }

        public double TargetValue
        {
            get { return state.TargetValue; }
        }

        public double StandardDeviation
        {
            get { return state.StandardDeviation; }
        }

        public int TestCode
        {
            get { return state.TestCode; }
        }

        public bool Installed
        {
            get { return state.Installed; }
        }

        public void Create(int testCode, double targetValue, double standardDeviation)
        {
            Apply(new QualityControlCreated(Guid.NewGuid(),testCode,targetValue,standardDeviation));
        }

        public void Update(double targetValue, double standardDeviation)
        {
            if (this.state.Installed)
            {
                Apply(new QualityControlUpdated(this.ControlId,targetValue,standardDeviation));
            }
            else
            {
                throw new InvalidOperationException("A non installed control cannot be updated.");
            }
        }

        private Guid ControlId
        {
            get { return state.ControlId; }
        }
       
    }
}