using System;
using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class GetEvaluationFor : ICommand
    {
        private readonly Guid qcResultId;

        public GetEvaluationFor(Guid qcResultId)
        {
            this.qcResultId = qcResultId;
        }

        public Guid QCResultId
        {
            get { return qcResultId; }
        }
    }
}