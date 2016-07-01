using System;

namespace QCRoutine.Domain
{
    public class QCResult
    {
        public QCResult(int testCode, double result, DateTime measuredDate)
        {
            this.Id = Guid.NewGuid();
            this.TestCode = testCode;
            this.Result = result;
            this.MeasuredTime = measuredDate;
        }

        public DateTime MeasuredTime { get; private set; }
        public int TestCode { get; private set; }
        public double Result { get; private set; }
        public Guid Id { get; private set; }
    }
}