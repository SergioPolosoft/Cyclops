using Application.Payloads;
using ApplicationServices;
using QCConfiguration.Application;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Responses;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Commands.Handlers;
using QCEvaluation.Application.PayloadMappers;
using QCEvaluation.Application.Ports;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Exceptions;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Events.Handlers
{
    public class QCResultReceivedHandler : IHandler<QCResultReceived>
    {
        private readonly IQCApplicationRepository qcApplicationRepository;
        private readonly IEvaluationsRepository evaluationsRepository;
        private readonly EvaluationDomainService evaluationService;
        private readonly IQCRuleRepository qcruleRepository;
        private readonly IQCResultsRepository resultsRepository;
        private readonly IQCConfigurationServicesPort qcConfigurationServices;

        public QCResultReceivedHandler(IQCApplicationRepository qcApplicationRepository, IEvaluationsRepository evaluationsRepository, IQCRuleRepository qcruleRepository, IQCResultsRepository resultsRepository, IQCConfigurationServicesPort qcConfigurationServices)
        {
            this.qcApplicationRepository = qcApplicationRepository;
            this.evaluationsRepository = evaluationsRepository;
            this.qcruleRepository = qcruleRepository;
            this.resultsRepository = resultsRepository;
            this.qcConfigurationServices = qcConfigurationServices;
            this.evaluationService = new EvaluationDomainService(resultsRepository);
        }

        public void Handle(QCResultReceived domainCommand)
        {
            var qcResultPayload = domainCommand.QCResult;
            
            var applicationQC = qcApplicationRepository.GetApplicationByTestCode(qcResultPayload.TestCode);
            if (applicationQC == null || applicationQC.IsNotInstalled)
            {
                throw new ApplicationNotExistsException();
            }

            QCResult qcResult = new QCResultMapper().Map(domainCommand.QCResult);
            resultsRepository.Add(qcResult);

            var qualityControlPayload = qcConfigurationServices.GetQualityControl(qcResultPayload.TestCode);

            EvaluationResult evaluation = EvaluationResult.NotEvaluated;

            if (qualityControlPayload != null)
            {
               var qualityControl = new QualityControlMapper().Map(qualityControlPayload);

                evaluation = evaluationService.Evaluate(qcResult, applicationQC.GetRulesEnabled(qcruleRepository), qualityControl);
            }

            evaluationsRepository.Add(new Evaluation(qcResult.Id, evaluation));
        }
    }
}