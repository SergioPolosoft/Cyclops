using ApplicationServices;
using QCConfiguration.Application.DTOs;

namespace QCConfiguration.Application.Commands
{
    public class ConfirmControlInstallation : ICommand
    {
        private readonly ControlDTO controlDTO;

        public ConfirmControlInstallation(ControlDTO controlDto)
        {
            this.controlDTO = controlDto;
        }

        public ControlDTO ControlDTO {
            get { return this.controlDTO; }
        }
    }
}