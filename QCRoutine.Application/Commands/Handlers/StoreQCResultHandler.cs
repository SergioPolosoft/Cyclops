using Application.Payloads;
using ApplicationServices;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Events;
using QCRoutine.Domain;

namespace QCRoutine.Application.Commands.Handlers
{
    public class StoreQCResultHandler:IHandler<StoreQCResult,IResponse>
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

        public IResponse Handle(StoreQCResult domainCommand)
        {
            var testCode = domainCommand.TestCode;

            var response = labconfiguration.Handle(new GetApplicationCommand(testCode));
            if (response.Status==CommandResult.Success)
            {
                var qcResult = new QCResult(testCode, domainCommand.Result, domainCommand.MeasuredDate);

                qcRepository.Add(qcResult);

                var qcResultPayload = new QCResultPayload(qcResult);

                qcConfiguration.Handle(new QCResultReceived(qcResultPayload));

                return new CommandSuccesfullyHandled();
            }
            else
            {
                return new CommandNotHandled(string.Format("Application code {0} does not exists.",testCode));
            }
        }
    }
}