using System;

namespace InstrumentCommunication.Application.Ports
{
    public class QCResultDTO
    {
        public QCResultDTO(int testCode, double result, DateTime measuredDate)
        {
            this.TestCode = testCode;
            this.Result = result;
            this.MeasurementDate = measuredDate;
        }

        public DateTime MeasurementDate { get; private set; }
        public int TestCode { get; private set; }
        public double Result { get; private set; }
        public Guid Id { get; private set; }
    }
}