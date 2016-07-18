using ApplicationServices;

namespace LabConfiguration.Application.Responses
{
    public class GetFTPConfigurationFailed : GetFTPConfigurationResponse
    {
        public GetFTPConfigurationFailed()
        {
            this.Status = CommandResult.Error;
            this.Message = LabConfigurationMessages.ErrorReadingFTPConfiguration;
        }
    }
}