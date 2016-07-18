using System;

namespace LabConfiguration.Domain
{
    public interface IApplicationRepository
    {
        void Add(Domain.ApplicationTest applicationTest);
        bool ApplicationExists(Guid id);
        void Remove(Guid id);
        void Update(Domain.ApplicationTest applicationTest);
        bool ApplicationCodeExists(int applicationCode);
        void RemoveByApplicationCode(int applicationCode);
        ApplicationTest GetByCode(int applicationCode);
        Guid GetApplicationId(int applicationCode);
    }
}