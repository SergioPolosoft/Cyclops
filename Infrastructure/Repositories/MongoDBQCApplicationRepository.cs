using System;
using System.Collections.Generic;
using ApplicationServices;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class MongoDBQCApplicationRepository : IQCApplicationRepository
    {
        private readonly IMongoDatabase database;

        private IMongoCollection<ApplicationEvent> Collection
        {
            get { return this.database.GetCollection<ApplicationEvent>("ApplicationEvents"); }
        }

        public MongoDBQCApplicationRepository()
        {
            var client = new MongoClient();
            this.database = client.GetDatabase("QCEvaluation");

            RegisterClass<QCRuleEnabled>();
            RegisterClass<QCRuleDisabled>();
        }

        private static void RegisterClass<T>()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)) == false)
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }

        

        public void Save(IList<AggreggateEvent> domainEvents)
        {
            foreach (var aggreggateEvent in domainEvents)
            {
                this.Collection.InsertOneAsync(aggreggateEvent as ApplicationEvent);
            }
        }

        private IEnumerable<AggreggateEvent> GetEventsById(Guid applicationId)
        {
            return this.Collection.FindSync(x => x.ApplicationId == applicationId).ToList();
        }

        public ApplicationQC GetApplication(Guid applicationId)
        {
            var events = GetEventsById(applicationId);
            return new ApplicationQC(events);
        }

        public ApplicationQC GetApplicationByTestCode(int testCode)
        {
            var creationEvent = this.Collection.OfType<ApplicationCreated>().FindSync(x => x.TestCode == testCode).FirstOrDefault();
            if (creationEvent != null)
            {
                return GetApplication(creationEvent.ApplicationId);
            }
            return null;
        }

        public void DeleteApplication(int applicationTestCode)
        {
            var creationEvent = this.Collection.OfType<ApplicationCreated>().FindSync(x => x.TestCode == applicationTestCode).FirstOrDefault();
            if (creationEvent != null)
            {
                this.Collection.DeleteManyAsync(x => x.ApplicationId == creationEvent.ApplicationId);
            }
        }
    }
}