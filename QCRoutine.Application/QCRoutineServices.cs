using System;
using System.Collections.Generic;
using ApplicationServices;
using QCRoutine.Application.Commands;
using QCRoutine.Application.Commands.Handlers;
using QCRoutine.Application.Ports;
using QCRoutine.Domain;
using IQCEvaluationPort = QCRoutine.Application.Ports.IQCEvaluationPort;

namespace QCRoutine.Application
{
    public class QCRoutineServices :ApplicationServicesBase, IQCRoutineServices
    {
        private readonly IQCEvaluationPort qcEvaluationServices;
        private readonly IQCResultsRepository resultsRepository;
        private readonly ILabConfigurationPort labconfiguration;

        public QCRoutineServices(IQCEvaluationPort qcEvaluationServices, IQCResultsRepository resultsRepository, ILabConfigurationPort labconfiguration)
        {
            this.qcEvaluationServices = qcEvaluationServices;
            this.resultsRepository = resultsRepository;
            this.labconfiguration = labconfiguration;
        }

        protected override Dictionary<Type, Func<ICommand,IResponse>> GetCommandHandlers()
        {
            return new Dictionary<Type, Func<ICommand, IResponse>>
            {
                {typeof(StoreQCResult),x=>new StoreQCResultHandler(qcEvaluationServices,resultsRepository, labconfiguration).Handle(x as StoreQCResult)},
                {typeof(GetResultsByDate),x=>new GetQCResultHandler(resultsRepository).Handle(x as GetResultsByDate)}
            };
        }

        protected override Dictionary<Type, Action<IEvent>> GetEventHandlers()
        {
            throw new NotImplementedException();
        }        
    }
}