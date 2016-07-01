using System.Collections.Generic;
using ApplicationServices;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Events;
using QCConfiguration.Domain.Repositories;

namespace QCConfiguration.Application.Commands.Handlers
{
    public class ConfirmControlInstallationHandler:IHandler<ConfirmControlInstallation,IResponse>
    {
        private readonly IQualityControlRepository qcrepository;

        public ConfirmControlInstallationHandler(IQualityControlRepository qcRepository)
        {
            this.qcrepository = qcRepository;
        }

        public IResponse Handle(ConfirmControlInstallation command)
        {
            var control = new QualityControl();
            control.Create(command.ControlDTO.TestCode, command.ControlDTO.Range1SD, command.ControlDTO.TargetValue);

            qcrepository.Add(control.Changes);
            return new CommandSuccesfullyHandled();
        }
    }
}