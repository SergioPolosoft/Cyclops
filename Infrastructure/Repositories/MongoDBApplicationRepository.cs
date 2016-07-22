using System;
using LabConfiguration.Domain;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MongoDBApplicationRepository:IApplicationRepository
    {
        private readonly IMongoDatabase database;

        public MongoDBApplicationRepository()
        {
            var client = new MongoClient();
            this.database = client.GetDatabase("LabConfiguration");
        }

        private IMongoCollection<ApplicationTest> Collection
        {
            get { return this.database.GetCollection<ApplicationTest>("ApplicationTests"); }
        }

        public void Add(ApplicationTest applicationTest)
        {
            this.Collection.InsertOneAsync(applicationTest);
        }

        public bool ApplicationExists(Guid id)
        {
            return this.Collection.FindSync(x => x.Id == id).Any();
        }

        public void Remove(Guid id)
        {
            this.Collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public void Update(ApplicationTest applicationTest)
        {
            this.Collection.FindOneAndReplaceAsync(x => x.Id == applicationTest.Id, applicationTest);
        }

        public bool ApplicationCodeExists(int applicationCode)
        {
            return this.Collection.FindSync(x => x.ApplicationCode == applicationCode).Any();
        }

        public void RemoveByApplicationCode(int applicationCode)
        {
            this.Collection.FindOneAndDeleteAsync(x => x.ApplicationCode == applicationCode);
        }

        public ApplicationTest GetByCode(int applicationCode)
        {
            return Collection.FindSync(x => x.ApplicationCode == applicationCode).FirstOrDefault();
        }

        public Guid GetApplicationId(int applicationCode)
        {
            return this.Collection.FindSync(x => x.ApplicationCode == applicationCode).First().Id;
        }
    }
}
