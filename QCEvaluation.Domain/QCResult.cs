using System;

namespace QCEvaluation.Domain
{
    public class QCResult
    {
        public Guid Id { get; set; }
        public int TestCode { get; set; }
        public double Result { get; set; }
    }
}