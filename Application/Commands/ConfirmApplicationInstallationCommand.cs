using ApplicationServices;

namespace LabConfiguration.Application.Commands
{
    public class ConfirmApplicationInstallationCommand : ICommand
    {
        private readonly ApplicationDTO applicationDtoToInstall;

        public ConfirmApplicationInstallationCommand(ApplicationDTO applicationDto)
        {
            this.applicationDtoToInstall = applicationDto;
        }

        public ApplicationDTO ApplicationDTO
        {
            get { return this.applicationDtoToInstall; }
        }        
    }
}