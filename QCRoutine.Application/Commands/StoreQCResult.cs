using System;
using ApplicationServices;

namespace QCRoutine.Application.Commands
{
    public class StoreQCResult : ICommand
    {
        public StoreQCResult(int testCode, double result, DateTime measuredDateTime)
        {
            this.TestCode = testCode;
            this.Result = result;
            this.MeasuredDate = measuredDateTime;
        }

        public double Result { get; private set; }

        public int TestCode { get; private set; }
        public DateTime MeasuredDate { get; private set; }
    }
}