using System;
using QCEvaluation.Application.Commands.Handlers;
using QCEvaluation.Domain;

namespace QCEvaluation.Application.Events
{
    public class QCResultBuilder:IBuilder<QCResult>
    {
        public QCResult Object
        {
            get; private set;
        }

        public QCResultBuilder BuildQCResult()
        {
            this.Object = new QCResult();
            return this;
        }

        public QCResultBuilder WithId(Guid resultId)
        {
            this.Object.Id = resultId;
            return this;
        }

        public QCResultBuilder ForTest(int testCode)
        {
            this.Object.TestCode = testCode;
            return this;
        }

        public QCResultBuilder WithValue(double value)
        {
            this.Object.Result = value;
            return this;
        }
    }
}