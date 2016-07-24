using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Infrastructure.Repositories;
using LabConfiguration.Adapters;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;

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
    }
}
