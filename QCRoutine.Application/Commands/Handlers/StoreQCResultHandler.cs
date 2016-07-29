using Application.Payloads;
using ApplicationServices;
using QCRoutine.Application.Ports;
using QCRoutine.Application.Responses;
using QCRoutine.Domain;

namespace QCRoutine.Application.Commands.Handlers
{
    public class StoreQCResultHandler:IHandler<StoreQCResult,StoreQCResultResponse>
    {
        private readonly IQCEvaluationPort qcEvaluationServices;
        private readonly IQCResultsRepository qcRepository;
        private readonly ILabConfigurationPort labconfiguration;

        public StoreQCResultHandler(IQCEvaluationPort qcEvaluationServices, IQCResultsRepository qcRepository, ILabConfigurationPort labconfiguration)
        {
            this.qcEvaluationServices = qcEvaluationServices;
            this.qcRepository = qcRepository;
            this.labconfiguration = labconfiguration;
        }

        public StoreQCResultResponse Handle(StoreQCResult domainCommand)
        {
            var testCode = domainCommand.TestCode;

            var application = labconfiguration.GetApplicationByTestCode(testCode);
            
            if (application == null)
            {
                return new QCResultNotStoredResponse(string.Format(QCRoutineMessages.ApplictionCodeDoesNotExists, testCode));
            }

            var qcResult = new QCResult(testCode, domainCommand.Result, domainCommand.MeasuredDate);

            qcRepository.Add(qcResult);

            var qcResultPayload = new QCResultPayload(qcResult);

            qcEvaluationServices.NotifyResultReceived(qcResultPayload);

            return new StoreQCResultResponse();
        }
    }
}