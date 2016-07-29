using System;
using ApplicationServices;

namespace InstrumentCommunication.Application.Commands
{
    public class StoreQCResult : ICommand
    {
        public StoreQCResult(int testCode, double result, DateTime measuredDateTime)
        {
            this.TestCode = testCode;
            this.Result = result;
            this.MeasuredDate = measuredDateTime;
        }

        internal double Result { get; private set; }

        internal int TestCode { get; private set; }
        public DateTime MeasuredDate { get; private set; }
    }
}