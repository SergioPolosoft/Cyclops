using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LabConfiguration.Domain
{
    public class Application
    {
        public Guid Id { get; private set; }
        public int ApplicationCode { get; private set; }

        public Application(int applicationToInstall)
        {
            this.Id = Guid.NewGuid();
            this.ApplicationCode = applicationToInstall;
        }
    }
}
