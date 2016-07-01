using System;
using System.Collections.Generic;
using ApplicationServices;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Commands.Handlers;
using QCConfiguration.Domain.Repositories;

namespace QCConfiguration.Application
{
    public class QCConfigurationServices:ApplicationServicesBase, IQCConfigurationServices
    {
        private readonly IQualityControlRepository qcRepository;

        public QCConfigurationServices(IQualityControlRepository qcRepository)
        {
            this.qcRepository = qcRepository;
        }

        protected override Dictionary<Type, Func<ICommand, IResponse>> GetCommandHandlers()
        {
            return new Dictionary<Type, Func<ICommand, IResponse>>()
            {
                {typeof(ConfirmControlInstallation), x=> new ConfirmControlInstallationHandler(qcRepository).Handle(x as ConfirmControlInstallation)},
                {typeof(GetQualityControl), x=>new GetQualityControlHandler().Handle(x as GetQualityControl)}
            };
        }

        protected override Dictionary<Type, Action<IEvent>> GetEventHandlers()
        {
            return new Dictionary<Type, Action<IEvent>>();
        }
    }
}