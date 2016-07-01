using System;
using ApplicationServices;

namespace LabConfiguration.Application.Commands
{
    public class ConfirmApplicationUpdateCommand : ICommand
    {
        private readonly int applicationCode;

        public ConfirmApplicationUpdateCommand(int applicationCode)
        {
            this.applicationCode = applicationCode;
        }

        public int ApplicationCode
        {
            get { return applicationCode; }
        }

        public Domain.Application Application { get; internal set; }
    }
}