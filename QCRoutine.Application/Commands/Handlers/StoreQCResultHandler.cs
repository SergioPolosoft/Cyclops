using Application.Payloads;
using ApplicationServices;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Events;
using QCRoutine.Application.Responses;
using QCRoutine.Application.Tests;
using QCRoutine.Domain;

namespace QCRoutine.Application.Commands.Handlers
{
    public class StoreQCResultHandler:IHandler<StoreQCResult,StoreQCResultResponse>
    {
        private readonly IQCEvaluationServices qcConfiguration;
        private readonly IQCResultsRepository qcRepository;
        private readonly ILabConfigurationServices labconfiguration;

        public StoreQCResultHandler(IQCEvaluationServices qcConfiguration, IQCResultsRepository qcRepository, ILabConfigurationServices labconfiguration)
        {
            this.qcConfiguration = qcConfiguration;
            this.qcRepository = qcRepository;
            this.labconfiguration = labconfiguration;
        }

        public StoreQCResultResponse Handle(StoreQCResult domainCommand)
        {
            var testCode = domainCommand.TestCode;

            var response = labconfiguration.Handle(new GetApplicationCommand(testCode));
            
            if (response is ApplicationDoesNotExistsResponse)
            {
                return new QCResultNotStoredResponse(string.Format(QCRoutineMessages.ApplictionCodeDoesNotExists, testCode));
            }

            var qcResult = new QCResult(testCode, domainCommand.Result, domainCommand.MeasuredDate);

            qcRepository.Add(qcResult);

            var qcResultPayload = new QCResultPayload(qcResult);

            qcConfiguration.Handle(new QCResultReceived(qcResultPayload));

            return new StoreQCResultResponse();
        }
    }
}