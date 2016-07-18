using System;
using ApplicationServices;

namespace LabConfiguration.Application.Responses
{
    public class ApplicationNotFound : GetApplicationResponse
    {
        public ApplicationNotFound(int testCode)
        {
            this.Status = CommandResult.Error;
            this.Message= String.Format(LabConfigurationMessages.ApplicationNotFound,testCode);
        }
    }
}