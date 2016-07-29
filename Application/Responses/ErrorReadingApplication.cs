using ApplicationServices;

namespace LabConfiguration.Application.Responses
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