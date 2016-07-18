using System;
using System.Runtime.InteropServices;
using Application.Payloads;
using ApplicationServices;
using LabConfiguration.Application.Responses;
using LabConfiguration.Domain;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class GetApplicationCommandHandler:IHandler<GetApplicationCommand,GetApplicationResponse>
    {
        private readonly IApplicationRepository applicationRepository;

        public GetApplicationCommandHandler(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public GetApplicationResponse Handle(GetApplicationCommand command)
        {
            GetApplicationResponse response = null;
            try
            {
                var application = applicationRepository.GetByCode(command.TestCode);
                if (application == null)
                {
                    response = new ApplicationNotFound(command.TestCode);
                }
                else
                {
                    response = new ApplicationFound(application);
                }

            }
            catch (Exception)
            {
                response = new ErrorReadingApplication(command.TestCode);
            }
            
            return response;
        }
    }

    public class ApplicationFound : GetApplicationResponse
    {
        public ApplicationFound(Domain.ApplicationTest applicationTest)
        {
            this.Application = new ApplicationPayload(applicationTest);
            this.Status = CommandResult.Success;
        }
    }
}