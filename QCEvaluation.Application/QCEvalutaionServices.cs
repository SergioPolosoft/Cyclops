using System;
using System.Collections.Generic;
using ApplicationServices;
using QCConfiguration.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Commands.Handlers;
using QCEvaluation.Application.DTOs;
using QCEvaluation.Application.Events;
using QCEvaluation.Application.Events.Handlers;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application
{
    public class QCEvalutaionServices:ApplicationServicesBase, IQCEvaluationServices
    {
        private readonly IQCRuleRepository qcRulesRepository;
        private readonly IQCApplicationRepository applicationRepository;
        private readonly IEvaluationsRepository evaluationsRepository;
        private readonly IQCResultsRepository qcResultsRepository;
        private readonly IQCConfigurationServices qcConfigurationServices;

        public QCEvalutaionServices(IQCRuleRepository qcRulesRepository, IQCApplicationRepository applicationRepository, IEvaluationsRepository evaluationsRepository, IQCResultsRepository qcResultsRepository, IQCConfigurationServices qcConfigurationServices)
        {
            this.qcRulesRepository = qcRulesRepository;
            this.applicationRepository = applicationRepository;
            this.evaluationsRepository = evaluationsRepository;
            this.qcResultsRepository = qcResultsRepository;
            this.qcConfigurationServices = qcConfigurationServices;
        }

        protected override Dictionary<Type, Func<ICommand,IResponse>> GetCommandHandlers()
        {
            return new Dictionary<Type, Func<ICommand, IResponse>>()
            {
                {typeof(CreateStandardDeviationRule), x=>new CreateStandardDeviationRuleHandler(qcRulesRepository).Handle(x as CreateStandardDeviationRule) },
                {typeof(ModifyStandardDeviationRule), x=>new ModifyStandardDeviationRuleHandler(qcRulesRepository).Handle(x as ModifyStandardDeviationRule) },
                {typeof(DeactivateQCRule), x=>new DeactivateQCRuleHandler(qcRulesRepository).Handle(x as DeactivateQCRule) },
                {typeof(ReactivateQCRule), x=>new ReactivateQCRuleHandler(qcRulesRepository).Handle(x as ReactivateQCRule) },
                {typeof(EnableRuleForApplication), x=>new EnableRuleForApplicationHandler(applicationRepository, qcRulesRepository).Handle(x as EnableRuleForApplication) },
                {typeof(DisableRuleForApplication), x=>new DisableRuleForApplicationHandler(applicationRepository).Handle(x as DisableRuleForApplication) },
                {typeof(GetEvaluationFor), x=>new GetEvaluationHandler(evaluationsRepository).Handle(x as GetEvaluationFor)}
            };
        }

        protected override Dictionary<Type, Action<IEvent>> GetEventHandlers()
        {
            return new Dictionary<Type, Action<IEvent>>()
            {
                {typeof(QCResultReceived), x=>new QCResultReceivedHandler(applicationRepository, evaluationsRepository, qcRulesRepository, qcResultsRepository, qcConfigurationServices).Handle(x as QCResultReceived) },
                {typeof(ApplicationInstalled), x=>new ApplicationInstalledHandler(applicationRepository).Handle(x as ApplicationInstalled) },
            };
        }

        public EvaluationDTO GetEvaluationOf(Guid qcResultId)
        {
            var evaluation = evaluationsRepository.GetEvaluationFor(qcResultId);
            return (EvaluationDTO)evaluation;
        }
    }
}