using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ApplicationServices;
using Infrastructure.Repositories;
using QCConfiguration.Application;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Responses;

namespace QCEvaluation.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class QCEvaluationService : IQCEvaluationService
    {
        private readonly QCEvaluationServices qcEvalutionService;

        public QCEvaluationService()
        {
            qcEvalutionService = new QCEvaluationServices(new MongoDBQCRulesRepository(),new InMemoryIQCApplicationRepository(), new HardCodedEvaluationsRepository(), new HardCodedQCResultsEvaluationRepository(), new QCConfigurationServices(new HardCodedQCRepository()));
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

        public GetStandardDeviationRuleResponse GetStandardDeviationRuleByName(string ruleName)
        {
            var result = qcEvalutionService.Handle(new GetStandardDeviationRuleByName(ruleName));

            return new GetStandardDeviationRuleResponse(((GetStandardDeviationRuleByNameResult)result).StandardDeviationRule);
        }
    }
}
