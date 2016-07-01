using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using QCConfiguration.Domain.Events;

namespace QCConfiguration.Domain
{
    public class QualityControl:Aggreggate<QualityControlState>
    {
        public QualityControl(IEnumerable<QualityControlEvent> aggreggateEvents) : base(new List<AggreggateEvent>())
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

        public void Create(int testCode, double targetValue, double standardDeviation)
        {
            Apply(new QualityControlCreated(Guid.NewGuid(),testCode,targetValue,standardDeviation));
        }
    }
}