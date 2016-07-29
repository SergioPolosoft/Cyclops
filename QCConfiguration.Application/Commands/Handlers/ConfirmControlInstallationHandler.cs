using ApplicationServices;
using QCConfiguration.Domain;
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
            var controlDto = command.ControlDTO;
            var control = qcrepository.GetQualityControlForTestCode(controlDto.TestCode);
            if (control!=null)
            {
                control.Update(controlDto.TargetValue, controlDto.StandardDeviation);
            }
            else
            {
                control = new QualityControl();
                control.Create(controlDto.TestCode, controlDto.StandardDeviation, controlDto.TargetValue);
            }
            

            qcrepository.Add(control.Changes);
            return new CommandSuccesfullyHandled();
        }
    }
}