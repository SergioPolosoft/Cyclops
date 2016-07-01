using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LabConfiguration.Domain;

namespace Infrastructure.Repositories
{
    public class HardCodedApplicationRepository : IApplicationRepository
    {
        private readonly Dictionary<Guid,Application> identityMap = new Dictionary<Guid, Application>();

        public void Add(Application application)
        {
            if (identityMap.ContainsKey(application.Id))
            {
                throw new InvalidDataException();
            }
            else
            {
                identityMap.Add(application.Id,application);
            }
        }

        public bool ApplicationExists(Guid id)
        {
            return this.identityMap.ContainsKey(id);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Application application)
        {
            throw new NotImplementedException();
        }

        public bool ApplicationCodeExists(int applicationCode)
        {
            return this.identityMap.Values.Any(x => x.ApplicationCode == applicationCode);
        }

        public void RemoveByApplicationCode(int applicationCode)
        {
            throw new NotImplementedException();
        }

        public Application GetByCode(int applicationCode)
        {
            return this.identityMap.Values.FirstOrDefault(x => x.ApplicationCode == applicationCode);
        }

        public Guid GetApplicationId(int applicationCode)
        {
            return GetByCode(applicationCode).Id;
        }
    }
}