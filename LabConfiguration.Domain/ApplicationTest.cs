using System;

namespace LabConfiguration.Domain
{
    public class ApplicationTest
    {
        public Guid Id { get; private set; }
        public int ApplicationCode { get; private set; }

        public ApplicationTest(int applicationToInstall)
        {
            this.Id = Guid.NewGuid();
            this.ApplicationCode = applicationToInstall;
        }
    }
}
