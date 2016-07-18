using ApplicationServices;

namespace QCConfiguration.Application.Responses
{
    public class QualityControlRepositoryFailed : GetQualityControlResponse
    {
        public QualityControlRepositoryFailed(int testCode) 
        {
            this.Message = string.Format(QCConfigurationMessages.ErrorReadingQCRepository, testCode);
            this.Status = CommandResult.Error;            
        }
    }
}