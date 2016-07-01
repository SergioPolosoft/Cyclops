using System;
using System.Collections.Generic;
using ApplicationServices;
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

        public FTPConfiguration GetFTPConfiguration()
        {
            return configurationRepository.GetFTPConfiguration();
        }        

        public Guid GetApplicationId(int testCode)
        {
            try
            {
                return applicationRepository.GetApplicationId(testCode);
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public bool ApplicationExists(int testCode)
        {
            return applicationRepository.ApplicationCodeExists(testCode);
        }

        protected override Dictionary<Type, Func<ICommand, IResponse>> GetCommandHandlers()
        {
            return new Dictionary<Type, Func<ICommand, IResponse>>()
            {
                { typeof(ConfirmApplicationInstallationCommand), x=> new ConfirmApplicationInstallationHandler(applicationRepository, qcevaluationServices).Handle(x as ConfirmApplicationInstallationCommand)},
                { typeof(ConfirmApplicationDeletionCommand), x=> new ConfirmApplicationDeletionHandler(applicationRepository).Handle(x as ConfirmApplicationDeletionCommand)},
                { typeof(ConfirmApplicationUpdateCommand), x=> new ConfirmApplicationUpdateHandler(applicationRepository).Handle(x as ConfirmApplicationUpdateCommand)},
                { typeof(StoreMainunitConfigurationCommand), x=> new StoreMainunitConfigurationHandler(configurationRepository).Handle(x as StoreMainunitConfigurationCommand)}
            };
        }

        protected override Dictionary<Type, Action<IEvent>> GetEventHandlers()
        {
            throw new NotImplementedException();
        }
    }
}