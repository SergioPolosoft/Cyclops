using ApplicationServices;
using LabConfiguration.Application.Responses;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class ErrorReadingApplication : GetApplicationResponse
    {
        public ErrorReadingApplication(int testCode)
        {
            this.Status = CommandResult.Error;            
            this.Message = string.Format(LabConfigurationMessages.ErrorReadingApplication,testCode);
        }
    }
}