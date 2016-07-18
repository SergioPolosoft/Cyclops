using System;
using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class GetEvaluation : ICommand
    {
        private readonly Guid qcResultId;

        public GetEvaluation(Guid qcResultId)
        {
            this.qcResultId = qcResultId;
        }

        public Guid QCResultId
        {
            get { return qcResultId; }
        }
    }
}