using System;
using QCRoutine.Domain;

namespace Application.Payloads
{
    public class QCResultPayload:IPayloadObject
    {
        private readonly QCResult qcResult;
        
        public QCResultPayload(QCResult qcResult)
        {
            this.qcResult = qcResult;
        }

        public double Value
        {
            get { return qcResult.Result; }
        }

        public Guid Id
        {
            get { return qcResult.Id; }
        }

        public int TestCode
        {
            get { return qcResult.TestCode; }
        }
    }
}