using System;
using System.Collections.Generic;
using System.Linq;
using Application.Payloads;
using ApplicationServices;
using LabConfiguration.Application;
using QCEvaluation.Application;
using QCRoutine.Application.Commands;
using QCRoutine.Application.Commands.Handlers;
using QCRoutine.Domain;

namespace QCRoutine.Application
{
    public class QCRoutineServices :ApplicationServicesBase, IQCRoutineServices
    {
        private readonly IQCEvaluationServices qcConfigurationServices;
        private readonly IQCResultsRepository resultsRepository;
        private readonly ILogger log;
        private readonly ILabConfigurationServices labconfiguration;

        public QCRoutineServices(IQCEvaluationServices qcConfigurationServices, IQCResultsRepository resultsRepository, ILogger log, ILabConfigurationServices labconfiguration)
        {
            this.qcConfigurationServices = qcConfigurationServices;
            this.resultsRepository = resultsRepository;
            this.log = log;
            this.labconfiguration = labconfiguration;
        }

        protected override Dictionary<Type, Func<ICommand,IResponse>> GetCommandHandlers()
        {
            return new Dictionary<Type, Func<ICommand, IResponse>>
            {
                {typeof(StoreQCResult),x=>new StoreQCResultHandler(qcConfigurationServices,resultsRepository, labconfiguration).Handle(x as StoreQCResult)}
            };
        }

        protected override Dictionary<Type, Action<IEvent>> GetEventHandlers()
        {
            throw new NotImplementedException();
        }

        public List<QCResultPayload> GetResultsByDate(int numberOfResults)
        {
            if (numberOfResults<=0)
            {
                throw new ArgumentOutOfRangeException("numberOfResults");
            }
            
            IEnumerable<QCResult> listOfQCResults = new List<QCResult>();
            try
            {
                listOfQCResults = resultsRepository.GetResultsOrderedByDate(numberOfResults);
            }
            catch (Exception exception)
            {
                log.Warning(exception);
            }

            return listOfQCResults.Select(qcResult => new QCResultPayload(qcResult)).ToList();
        }
    }
}