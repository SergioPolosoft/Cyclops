using System;
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
            applicationRepository.GetByCode(command.TestCode);
            return null;
        }
    }
}