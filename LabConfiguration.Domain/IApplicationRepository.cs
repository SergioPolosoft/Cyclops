using System;

namespace LabConfiguration.Domain
{
    public interface IApplicationRepository
    {
        void Add(Domain.Application application);
        bool ApplicationExists(Guid id);
        void Remove(Guid id);
        void Update(Domain.Application application);
        bool ApplicationCodeExists(int applicationCode);
        void RemoveByApplicationCode(int applicationCode);
        Application GetByCode(int applicationCode);
        Guid GetApplicationId(int applicationCode);
    }
}