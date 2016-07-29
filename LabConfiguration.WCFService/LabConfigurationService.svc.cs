using ApplicationServices;
using Infrastructure.Repositories;
using LabConfiguration.Adapters;
using LabConfiguration.Adapters.Service_References.QCEvaluationServiceReference;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;
using LabConfiguration.Application.Commands.Handlers;
using LabConfiguration.Application.Responses;

namespace LabConfiguration.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class LabConfigurationService : ILabConfigurationService
    {
        private readonly Application.LabConfigurationService labConfigurationService;

        public LabConfigurationService()
        {
            labConfigurationService = new Application.LabConfigurationService(new HardCodedConfigurationRepository(), new MongoDBApplicationRepository(),new QCEvaluationAdapter());    
        }

        public ConfirmApplicationInstallationResponse ConfirmApplicationInstallation(ConfirmApplicationInstallationRequest request)
        {
            labConfigurationService.Handle(new ConfirmApplicationInstallationCommand(new ApplicationDTO(request.ApplicationTestCode)));
            return new ConfirmApplicationInstallationResponse();
        }

        public GetApplicationByTestCodeResponse GetApplicationByTestCode(int testCode)
        {
            var commandResponse = labConfigurationService.Handle(new GetApplicationCommand(testCode));
            var applicationFound = commandResponse as ApplicationFound;
            
            if (applicationFound!=null)
            {
                return new GetApplicationByTestCodeResponse()
                {
                    Status = RequestResult.Succesfull,
                    Application = new ApplicationData()
                    {
                        TestCode = applicationFound.Application.TestCode,
                    }
                };
            }
            return new GetApplicationByTestCodeResponse(){Status = RequestResult.Error};
        } 
    }
}
