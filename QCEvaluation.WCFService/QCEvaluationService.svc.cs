using Application.Payloads;
using ApplicationServices;
using Infrastructure.Repositories;
using QCEvaluation.Adapters;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Events;
using WCFServices.Common;

namespace QCEvaluation.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class QCEvaluationService : IQCEvaluationService
    {
        private readonly QCEvaluationServices qcEvalutionService;

        public QCEvaluationService()
        {
            qcEvalutionService = new QCEvaluationServices(new MongoDBQCRulesRepository(), new MongoDBQCApplicationRepository(), new MongoDBQCEvaluationsRepository(), new HardCodedQCResultsEvaluationRepository(), new QCConfigurationServicesAdapter());
        }
        
        public CreateStandardDeviationRuleResponse CreateStandardDeviationRule(CreateStandardDeviationRuleRequest request)
        {
            var createStandardDeviationRule = new CreateStandardDeviationRule(request.RuleName, request.WithinControlValue, request.Comments, request.NumberOfControls, request.StandardDeviationLimits);
            var response = qcEvalutionService.Handle(createStandardDeviationRule);
            if (response.Status== CommandResult.Success)
            {
                return new CreateStandardDeviationRuleResponse(RequestResult.Succesfull, response.Message);
            }
            return new CreateStandardDeviationRuleResponse(RequestResult.Error, response.Message);
        }

        public void NotifyApplicationInstalled(int testCode)
        {
            qcEvalutionService.Handle(new ApplicationInstalled(testCode));
        }

        public void NotifyResultReceived(QCResultDTO qcResultDto)
        {
            var qcResultPayload = new QCResultPayload
            {
                Id = qcResultDto.Id,
                TestCode = qcResultDto.TestCode,
                Value = qcResultDto.Value
            };

            qcEvalutionService.Handle(new QCResultReceived(qcResultPayload));
        }
    }
}
