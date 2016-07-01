using ApplicationServices;
using QCConfiguration.Application;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Responses;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Commands.Handlers;
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
        private readonly IQCConfigurationServices qcConfigurationServices;

        public QCResultReceivedHandler(IQCApplicationRepository qcApplicationRepository, IEvaluationsRepository evaluationsRepository, IQCRuleRepository qcruleRepository, IQCResultsRepository resultsRepository, IQCConfigurationServices qcConfigurationServices)
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

            var response = qcConfigurationServices.Handle(new GetQualityControl(qcResultPayload.TestCode));

            QualityControlPayload qualityControlPayload = ((GetQualityControlResponse)response).QualityControl;

            QCResult qcResult = new QCResultMapper().Map(domainCommand.QCResult);

            resultsRepository.Add(qcResult);
            
            var evaluation = evaluationService.Evaluate(qcResult, applicationQC.GetRulesEnabled(qcruleRepository), qualityControlPayload);

            evaluationsRepository.Add(new Evaluation(qcResult.Id, evaluation));
        }
    }
}