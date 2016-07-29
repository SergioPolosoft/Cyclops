using System;
using QCRoutine.Domain;

namespace Application.Payloads
{
    public class QCResultPayload:IPayloadObject
    {
        private readonly QCResult qcResult;
        private int testCode;
        private double value;
        private Guid id;

        public QCResultPayload(QCResult qcResult)
        {
            this.qcResult = qcResult;
        }

        public QCResultPayload()
        {
            
        }

        public double Value
        {
            get { return qcResult==null?value:qcResult.Result; }
            set { this.value = value; }
        }

        public Guid Id
        {
            get { return qcResult==null?this.id:qcResult.Id; }
            set { id = value; }
        }

        public int TestCode
        {
            get { return qcResult==null?testCode:qcResult.TestCode; }
            set { testCode = value; }
        }
    }
}