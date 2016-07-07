using System;
using System.Collections.Generic;
using ApplicationServices;
using InstrumentCommunication.Application.Commands;
using LabConfiguration.Application.Commands;
using LabConfiguration.Application.Commands.Handlers;
using LabConfiguration.Domain;
using QCEvaluation.Application;

namespace LabConfiguration.Application
{
    public class LabConfigurationService : ApplicationServicesBase, ILabConfigurationServices
    {
        private readonly IConfigurationRepository configurationRepository;
        private readonly IApplicationRepository applicationRepository;
        private readonly IQCEvaluationServices qcevaluationServices;

        public LabConfigurationService(IConfigurationRepository configurationRepository, IApplicationRepository applicationRepository, IQCEvaluationServices qcevaluationServices)
        {
            this.configurationRepository = configurationRepository;
            this.applicationRepository = applicationRepository;
            this.qcevaluationServices = qcevaluationServices;
        }

        protected override Dictionary<Type, Func<ICommand, IResponse>> GetCommandHandlers()
        {
            return new Dictionary<Type, Func<ICommand, IResponse>>()
            {
                { typeof(ConfirmApplicationInstallationCommand), x=> new ConfirmApplicationInstallationHandler(applicationRepository, qcevaluationServices).Handle(x as ConfirmApplicationInstallationCommand)},
                { typeof(ConfirmApplicationDeletionCommand), x=> new ConfirmApplicationDeletionHandler(applicationRepository).Handle(x as ConfirmApplicationDeletionCommand)},
                { typeof(ConfirmApplicationUpdateCommand), x=> new ConfirmApplicationUpdateHandler(applicationRepository).Handle(x as ConfirmApplicationUpdateCommand)},
                { typeof(StoreMainunitConfigurationCommand), x=> new StoreMainunitConfigurationHandler(configurationRepository).Handle(x as StoreMainunitConfigurationCommand)},
                { typeof(GetFTPConfiguration), x=> new GetFTPConfigurationHandler(configurationRepository).Handle(x as GetFTPConfiguration)},
                { typeof(GetApplicationCommand), x=> new GetApplicationCommandHandler(applicationRepository).Handle(x as GetApplicationCommand)}
            };
        }

        protected override Dictionary<Type, Action<IEvent>> GetEventHandlers()
        {
            throw new NotImplementedException();
        }
    }
}