using System.Collections.Generic;
using ApplicationServices;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Events;
using QCConfiguration.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class MongoDBQCRepository:IQualityControlRepository
    {
        private readonly IMongoDatabase database;

        public MongoDBQCRepository()
        {
            var client = new MongoClient();
            this.database = client.GetDatabase("QCConfiguration");

            RegisterClass<QualityControlCreated>();
            RegisterClass<QualityControlUpdated>();
        }

        private static void RegisterClass<T>()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)) == false)
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }

        public QualityControl GetQualityControlForTestCode(int testCode)
        {
            var creationEvent = Collection.OfType<QualityControlCreated>().FindSync(x => x.TestCode == testCode).FirstOrDefault();
            if (creationEvent!=null)
            {
                var events = Collection.FindSync(x => x.ControlId == creationEvent.ControlId).ToList();
                return new QualityControl(events);
            }
            return null;
        }

        public void Add(IList<AggreggateEvent> changes)
        {
            foreach (var aggreggateEvent in changes)
            {
                Collection.InsertOneAsync(aggreggateEvent as QualityControlEvent);
            }
        }

        private IMongoCollection<QualityControlEvent> Collection
        {
            get { return this.database.GetCollection<QualityControlEvent>("QualityContolEvents"); }
        }
    }
}
