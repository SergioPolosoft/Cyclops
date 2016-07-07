using ApplicationServices;

namespace QCConfiguration.Application.Responses
{
    public class QualityControlNotFound : GetQualityControlResponse
    {
        public QualityControlNotFound(int testCode)
        {
            this.Message = string.Format(QCConfigurationMessages.QCNotExists, testCode);
            this.Status = CommandResult.Error;
        }
    }
}