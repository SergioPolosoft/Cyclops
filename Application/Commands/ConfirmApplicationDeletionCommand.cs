using ApplicationServices;

namespace LabConfiguration.Application.Commands
{
    public class ConfirmApplicationDeletionCommand : ICommand
    {
        private readonly int applicationToDelete;

        public ConfirmApplicationDeletionCommand(int applicationGuid)
        {
            this.applicationToDelete = applicationGuid;
        }

        public int ApplicationCode
        {
            get { return applicationToDelete; }
        }
    }
}