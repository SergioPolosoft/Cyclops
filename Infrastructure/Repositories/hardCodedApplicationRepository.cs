using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LabConfiguration.Domain;

namespace Infrastructure.Repositories
{
    public class HardCodedApplicationRepository : IApplicationRepository
    {
        private readonly Dictionary<Guid,ApplicationTest> identityMap = new Dictionary<Guid, ApplicationTest>();

        public void Add(ApplicationTest applicationTest)
        {
            if (identityMap.ContainsKey(applicationTest.Id))
            {
                throw new InvalidDataException();
            }
            identityMap.Add(applicationTest.Id,applicationTest);
        }

        public bool ApplicationExists(Guid id)
        {
            return this.identityMap.ContainsKey(id);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationTest applicationTest)
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

        public ApplicationTest GetByCode(int applicationCode)
        {
            return this.identityMap.Values.FirstOrDefault(x => x.ApplicationCode == applicationCode);
        }

        public Guid GetApplicationId(int applicationCode)
        {
            return GetByCode(applicationCode).Id;
        }
    }
}